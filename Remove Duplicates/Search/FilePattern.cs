//
//    Remove Duplicates
//    Copyright (C) 2021 Timothy Baxendale
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Baxendale.Data.Collections;
using Baxendale.Data.Xml;
using Baxendale.RemoveDuplicates.Native;

namespace Baxendale.RemoveDuplicates.Search
{
    internal class FilePattern : ICollection<string>, IReadOnlyCollection<string>, IEquatable<FilePattern>, IXmlSerializableObject
    {
        private const string GENERIC_CUSTOM_DESCRIPTION = "Custom";

        private static readonly IEqualityComparer<string> MaskComparer = StringComparer.CurrentCultureIgnoreCase;

        private static readonly string[] ALL_FILES_MASK = new string[] { "*.*" };
        private static readonly char[] ValidMaskChars = { '*', '?' };

        public static readonly FilePattern AllFiles = new FilePattern() { _description = "All Files", _subpatterns = ALL_FILES_MASK };

        public static readonly FilePattern BitmapFiles     = new FilePattern("Bitmap Files", "*.bmp", "*.dib");
        public static readonly FilePattern JpegFiles       = new FilePattern("JPEG Files", "*.jpg", "*.jpeg", "*.jpe", "*.jfif");
        public static readonly FilePattern GifFiles        = new FilePattern("GIF Files", "*.gif");

        public static readonly FilePattern SvgFiles        = new FilePattern("Scalable Vector Graphics Files", "*.svg");
        public static readonly FilePattern IconFiles       = new FilePattern("Icon Files", "*.ico");
        public static readonly FilePattern AllImageFiles   = new FilePattern("All Image Files", BitmapFiles, JpegFiles, GifFiles, IconFiles);

        public static readonly FilePattern PlainTextFiles  = new FilePattern("Plain Text Files", "*.txt", "*.csv");
        public static readonly FilePattern RichTextFiles   = new FilePattern("Rich Text Files", "*.rtf");
        public static readonly FilePattern AllTextFiles    = new FilePattern("All Text Files", PlainTextFiles, RichTextFiles);

        public static readonly FilePattern WordFiles       = new FilePattern("Word Documents", "*.docx", "*.docm", "*.dotx", "*.dotm", "*.doc");
        public static readonly FilePattern ExcelFiles      = new FilePattern("Excel Files", "*.xlsx", "*.xlsm", "*.xlsb", "*.xlam", "*.xltx", "*.xltm", "*.xls", "*.xla", "*.xlt", "*.xlm", "*xlw");
        public static readonly FilePattern PowerPointFiles = new FilePattern("PowerPoint Presentations", "*.pptx", "*.ppt", "*.pptm", "*.ppsx", "*.pps", "*.ppsm", "*.potx", "*.pot", "*.potm");
        public static readonly FilePattern MsOfficeFiles   = new FilePattern("Microsoft Office Files", WordFiles, ExcelFiles, PowerPointFiles);

        private static char[] _invalidMaskChars;

        private static char[] InvalidMaskCharacters
        {
            get
            {
                if (_invalidMaskChars == null)
                {
                    char[] invalidFilenameChars = Path.GetInvalidFileNameChars();
                    _invalidMaskChars = new char[Math.Max(invalidFilenameChars.Length - ValidMaskChars.Length, 0)];
                    int idx = 0;
                    foreach (char invalidFilenameChar in invalidFilenameChars)
                    {
                        if (Array.IndexOf(ValidMaskChars, invalidFilenameChar) < 0)
                            _invalidMaskChars[idx++] = invalidFilenameChar;
                    }
                }
                return _invalidMaskChars;
            }
        }

        private string[] _subpatterns;
        private string _description;

        public string Description
        {
            get
            {
                return _description ?? string.Empty;
            }
        }

        public string FullPattern
        {
            get
            {
                if (_subpatterns == null || _subpatterns.Length == 0)
                    return ALL_FILES_MASK[0];
                if (_subpatterns.Length == 1)
                    return _subpatterns[0];
                return string.Join(";", _subpatterns);
            }
        }

        public int Count
        {
            get
            {
                return _subpatterns.Length;
            }
        }

        public IReadOnlyCollection<string> Subpatterns
        {
            get
            {
                if (_subpatterns == null)
                {
                    return ALL_FILES_MASK.AsReadOnly();
                }
                else
                {
                    return _subpatterns.AsReadOnly();
                }
            }
        }

        private FilePattern()
        {
        }

        public FilePattern(string description, string fullPattern)
        {
            _description = description;
            if (fullPattern != null)
                _subpatterns = GetUniqueMasks(fullPattern.Split(';'));
        }

        public FilePattern(string fullPattern)
            : this(GENERIC_CUSTOM_DESCRIPTION, fullPattern)
        {
        }

        public FilePattern(string description, FilePattern pattern1, FilePattern pattern2, params FilePattern[] patterns)
            : this(description, pattern1.Singleton().Concat(pattern2.Singleton()).Concat(patterns))
        {
        }

        public FilePattern(string description, IEnumerable<FilePattern> patterns)
        {
            if (patterns == null) throw new ArgumentNullException(nameof(patterns));

            IEnumerable<string> subpatterns = patterns.Select(p => p._subpatterns.AsEnumerable())
                                                      .Aggregate((a, next) => a.Concat(next));
            _description = description;
            _subpatterns = GetUniqueMasks(subpatterns);
        }

        public FilePattern(string description, string subpattern1, string subpattern2, params string[] subpatterns)
            : this(description, subpattern1.Singleton().Concat(subpattern2.Singleton()).Concat(subpatterns))
        {
        }

        public FilePattern(string description, IEnumerable<string> subpatterns)
        {
            if (subpatterns == null) throw new ArgumentNullException(nameof(subpatterns));
            _description = description;
            _subpatterns = GetUniqueMasks(subpatterns);
        }

        private static string[] GetUniqueMasks(IEnumerable<string> enumerable, params string[] masks)
        {            
            ISet<string> subpatternSet = new HashSet<string>(enumerable, MaskComparer);
            subpatternSet.UnionWith(masks);
            subpatternSet.Remove(null);

            if (subpatternSet.Count == 0)
                return ALL_FILES_MASK;

            int index = 0;
            string[] uniqueMasks = new string[subpatternSet.Count];
            foreach(string mask in masks.Concat(enumerable))
            {
                if (subpatternSet.Remove(mask))
                {
                    ValidateMask(mask);
                    uniqueMasks[index++] = mask;
                }
            }
            return uniqueMasks;
        }

        private static void ValidateMask(string mask)
        {
            if (mask == null) throw new ArgumentNullException(nameof(mask));

            int invalidChar = mask.IndexOfAny(InvalidMaskCharacters);
            if (invalidChar > -1)
                throw new ArgumentException($"Pattern contains an invalid character: '{mask[invalidChar]}'", mask[invalidChar].ToString());
        }

        public bool Matches(string fileName)
        {
            HRESULT hresult;
            if (_subpatterns.Length == 1)
            {
                hresult = Win32.PathMatchSpecEx(fileName, FullPattern, PMSF.NORMAL);
            }
            else
            {
                hresult = Win32.PathMatchSpecEx(fileName, FullPattern, PMSF.MULTIPLE | PMSF.DONT_STRIP_SPACES);
            }
            return hresult == HRESULT.S_OK;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Description))
                return FullPattern;
            return $"{Description} ({FullPattern})";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FilePattern);
        }

        public override int GetHashCode()
        {
            return FullPattern.GetHashCode() ^ Description.GetHashCode();
        }

        public bool Equals(FilePattern other)
        {
            return this == other;
        }

        public static FilePattern operator +(FilePattern left, FilePattern right)
        {
            return new FilePattern(left.Description, left, right);
        }

        public static bool operator ==(FilePattern left, FilePattern right)
        {
            if (left == (object)right) return true;
            if (left == (object)null || right == (object)null) return false;
            return left.Description == right.Description && left._subpatterns.EqualsArray(right._subpatterns, MaskComparer);
        }

        public static bool operator !=(FilePattern left, FilePattern right)
        {
            return !(left == right);
        }

        public XElement ToXml(XName name)
        {
            XElement node = new XElement(name);
            node.SetAttributeValue("description", Description);
            foreach(string mask in _subpatterns)
            {
                XElement pattern = new XElement("pattern");
                pattern.SetAttributeValue("mask", mask);
                node.Add(pattern);
            }
            return node;
        }

        public static FilePattern FromXml(XElement node)
        {
            string desc = node.Attribute("description")?.Value;
            var subpatterns = node.Elements("pattern").Select(o => o.Attribute("mask")?.Value);
            return new FilePattern(desc, subpatterns);
        }

        #region ICollection<string>

        bool ICollection<string>.IsReadOnly
        {
            get
            {
                return true;
            }
        }

        void ICollection<string>.Add(string item)
        {
            throw new NotSupportedException();
        }

        void ICollection<string>.Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(string pattern)
        {
            return Array.IndexOf(_subpatterns, pattern) > -1;
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            Array.Copy(_subpatterns, array, arrayIndex);
        }

        bool ICollection<string>.Remove(string item)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region IEnumerable<string>

        public IEnumerator<string> GetEnumerator()
        {
            return Subpatterns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
