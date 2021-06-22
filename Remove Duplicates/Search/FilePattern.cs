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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baxendale.RemoveDuplicates.Search
{
    internal class FilePattern : IEquatable<FilePattern>
    {
        public static readonly FilePattern AllFiles      = new FilePattern("All Files", "*.*");

        public static readonly FilePattern BitmapFiles   = new FilePattern("Bitmap Files", "*.bmp;*.dib");
        public static readonly FilePattern JpegFiles     = new FilePattern("JPEG Files", "*.jpg;*.jpeg;*.jpe;*.jfif");
        public static readonly FilePattern GifFiles      = new FilePattern("JPEG Files", "*.gif");
        public static readonly FilePattern TiffFiles     = new FilePattern("Tag Image Files", "*.tif;tiff");
        public static readonly FilePattern HeicFiles     = new FilePattern("High Efficiency Image Files", "*.heic");
        public static readonly FilePattern WebpFiles     = new FilePattern("WebP Files", "*.webp");
        public static readonly FilePattern IconFiles     = new FilePattern("Icon Files", "*.ico");
        public static readonly FilePattern AllImageFiles = new FilePattern("All Image Files", BitmapFiles, JpegFiles, GifFiles, TiffFiles, HeicFiles, WebpFiles, IconFiles);

        public static readonly FilePattern TextFiles     = new FilePattern("Plain Text Files", "*.txt");

        public string Description { get; set; }
        public string Pattern { get; set; }

        public FilePattern(string description, string pattern)
        {
            Description = description;
            Pattern = pattern;
        }

        public FilePattern(string pattern)
            : this("Custom Pattern", pattern)
        {
        }

        public FilePattern(string description, FilePattern pattern1, FilePattern pattern2, params FilePattern[] patterns)
        {
            if (pattern1 == null) throw new ArgumentNullException(nameof(pattern1));
            if (pattern2 == null) throw new ArgumentNullException(nameof(pattern2));
            if (patterns == null) throw new ArgumentNullException(nameof(patterns));

            Description = description;
            Pattern = BuildPattern((new[] { pattern1, pattern2 }).Concat(patterns));
        }

        private static string BuildPattern(IEnumerable<FilePattern> patterns)
        {
            StringBuilder sbPattern = new StringBuilder();
            using (IEnumerator<FilePattern> e = patterns.GetEnumerator())
            {
                if (e.MoveNext() && e.Current?.Pattern != null)
                    sbPattern.Append(e.Current.Pattern);
                while (e.MoveNext())
                {
                    if (e.Current?.Pattern != null)
                    {
                        sbPattern.Append(';');
                        sbPattern.Append(e.Current.Pattern);
                    }
                }
            }
            return sbPattern.ToString();
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Description))
                return Pattern;
            return $"{Description} ({Pattern})";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FilePattern);
        }

        public override int GetHashCode()
        {
            return (Pattern?.GetHashCode() ?? 0) ^ (Description?.GetHashCode() ?? 0);
        }

        public bool Equals(FilePattern other)
        {
            if (other == null)
                return false;
            if (other == (object)this)
                return true;
            return other.Description == Description && other.Pattern == Pattern;
        }
    }
}
