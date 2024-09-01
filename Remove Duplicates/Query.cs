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
using Baxendale.Data.Xml;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates
{
    internal class Query : IXmlSerializableObject
    {
        private List<string> _paths;

        [XmlSerializableProperty(Name = "paths", ElementName = "path", AttributeName = "uri", BackingField = nameof(_paths))]
        public IEnumerable<string> SearchPaths => _paths;

        [XmlSerializableProperty(Name = "patterns")]
        public FilePattern Pattern { get; set; }

        [XmlSerializableProperty(Name = "subdirs")]
        public bool IncludeSubdirectories { get; set; }

        private Query()
        {
        }

        public Query(IEnumerable<string> paths, FilePattern pattern = null, bool subdirs = true)
        {
            _paths = new List<string>(paths);
            Pattern = pattern ?? FilePattern.AllFiles;
            IncludeSubdirectories = subdirs;
        }
    }
}
