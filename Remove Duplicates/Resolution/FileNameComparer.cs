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
using System.Collections.Generic;
using System.IO;
using Baxendale.Data.Xml;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates.Resolution
{
    internal class FileNameComparer : IFileComparer
    {
        [XmlSerializableProperty(Name = "matches")]
        private List<FilePattern> _patterns;

        [XmlDoNotSerialize]
        public IEnumerable<FilePattern> Patterns
        {
            get
            {
                return _patterns.AsReadOnly();
            }
        }

        [XmlSerializableProperty(Name = "reverse", SerializeDefault = false)]
        public bool Reverse { get; set; }

        public FileNameComparer()
        {
            _patterns = new List<FilePattern>();
        }

        public FileNameComparer(IEnumerable<FilePattern> patterns)
        {
            _patterns = new List<FilePattern>(patterns);
        }

        public int Compare(FileInfo x, FileInfo y)
        {
            int p1 = int.MaxValue;
            int p2 = int.MaxValue;
            for (int i = _patterns.Count - 1; i >= 0; --i)
            {
                if (_patterns[i].Matches(x.Name))
                    p1 = i;
                if (_patterns[i].Matches(y.Name))
                    p2 = i;
            }
            return p1.CompareTo(p2);
        }
    }
}
