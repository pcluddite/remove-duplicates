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
using System.Linq;
using System.Xml.Linq;
using Baxendale.Data.Xml;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates
{
    internal class Query : IXmlSerializableObject
    {
        public IEnumerable<string> SearchPaths { get; private set; }
        public FilePattern Pattern { get; set; }

        public Query(IEnumerable<string> paths, FilePattern pattern)
        {
            SearchPaths = new List<string>(paths);
            Pattern = pattern;
        }

        public XElement ToXml(XmlSerializer serializer, XName name)
        {
            XElement node = new XElement(name);
            XElement paths = new XElement("paths");
            foreach (string path in SearchPaths)
            {
                XElement pathNode = new XElement("path");
                pathNode.SetAttributeValue("uri", path);
                paths.Add(pathNode);
            }
            node.Add(paths);
            node.Add(serializer.Serialize(Pattern, "patterns"));
            return node;
        }

        public static Query FromXml(XmlSerializer serializer, XElement node)
        {
            List<string> paths = new List<string>();

            XElement pathsNode = node.Element("paths");
            IEnumerable<XElement> pathNodeList;
            if (pathsNode == null || !(pathNodeList = pathsNode.Elements("path")).Any())
                throw new XmlSerializationException(node, "There are no paths listed in this query file");

            foreach (XElement pathNode in pathNodeList)
            {
                string uri = pathNode.Attribute("uri")?.Value;
                if (uri != null)
                {
                    paths.Add(uri);
                }
            }

            XElement patternsNode = node.Element("patterns");
            if (patternsNode == null || !patternsNode.Elements("pattern").Any())
                throw new XmlSerializationException(node, "There are no patterns to match in this query file");
            
            FilePattern pattern = serializer.Deserialize<FilePattern>(patternsNode);
            return new Query(paths, pattern);
        }
    }
}
