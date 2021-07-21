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
using System.Linq;
using Baxendale.Data.Collections.ReadOnly;
using Baxendale.Data.Xml;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates.Resolution
{
    internal class FileResolution : IXmlSerializableObject
    {
        private List<FileInfo> _originals;
        private List<FileInfo> _duplicates;

        [XmlSerializableProperty(Name = "hash")]
        public Md5Hash Hash { get; private set; }

        [XmlSerializableProperty(Name = "originals", ElementName = "file", BackingField = nameof(_originals))]
        public ICollection<FileInfo> Originals => new ReadOnlyCollection<FileInfo>(_originals);

        [XmlSerializableProperty(Name = "duplicates", ElementName = "file", BackingField = nameof(_duplicates))]
        public ICollection<FileInfo> Duplicates => new ReadOnlyCollection<FileInfo>(_duplicates);

        public FileResolution(Md5Hash hash, IEnumerable<FileInfo> originals, IEnumerable<FileInfo> duplicates)
        {
            _originals = originals.ToList();
            _duplicates = duplicates.ToList();
            Hash = hash;
        }
    }
}
