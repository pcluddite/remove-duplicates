//
//    Remove Duplicates
//    Copyright (C) 2021-2024 Timothy Baxendale
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
using Baxendale.Data.Xml;

namespace Baxendale.RemoveDuplicates.Search
{
    internal class SearchResult : IXmlSerializableObject
    {
        private readonly List<UniqueFile> _files;
        
        [XmlSerializableProperty(Name = "query")]
        public Query Query { get; private set; }


        [XmlSerializableProperty(Name = "found", BackingField = nameof(_files), ElementName = "file")]
        public IEnumerable<UniqueFile> Files
        {
            get => _files.AsReadOnly();
        }

        private SearchResult()
        {
        }

        public SearchResult(Query query, IEnumerable<UniqueFile> files)
        {
            Query = query;
            _files = new List<UniqueFile>(files);
        }
    }
}
