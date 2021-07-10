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
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates.Resolution
{
    internal class FileDirectoryComparer : IFileComparer
    {
        private Dictionary<string, int> _dirPriority = new Dictionary<string, int>(PathEqualityComparer.Default);

        public IEnumerable<string> Directories
        {
            get
            {
                foreach (KeyValuePair<string, int> kv in _dirPriority.OrderBy(o => o.Value))
                    yield return kv.Key;
            }
        }

        public bool Reverse { get; set; }

        public FileDirectoryComparer(IEnumerable<string> directories)
        {
            int p = 0;
            foreach(string dir in directories)
            {
                _dirPriority.Add(dir, p++);
            }
        }
        
        public int Compare(FileInfo x, FileInfo y)
        {
            int p1, p2;
            if (!_dirPriority.TryGetValue(x.DirectoryName, out p1)) throw new ArgumentException("Directory has not been prioritized", nameof(x));
            if (!_dirPriority.TryGetValue(y.DirectoryName, out p2)) throw new ArgumentException("Directory has not been prioritized", nameof(y));
            return p1.CompareTo(p2);
        }

        public XElement ToXml(XName name)
        {
            XElement node = new XElement(name);
            if (Reverse)
                node.SetAttributeValue("reverse", Reverse);
            foreach(string dir in Directories)
            {
                XElement dirNode = new XElement("directory");
                dirNode.SetAttributeValue("uri", dir);
                node.Add(dirNode);
            }
            return node;
        }

        public static FileDirectoryComparer FromXml(XElement node)
        {
            return new FileDirectoryComparer(node.Elements("directory")
                .Select(dirNode => dirNode.Attribute("uri")?.Value)
                .Where(path => path != null))
            {
                Reverse = bool.TrueString.EqualsIgnoreCase(node.Attribute("reverse")?.Value)
            };
        }
    }
}
