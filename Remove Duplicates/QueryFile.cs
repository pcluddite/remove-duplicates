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
using System.Xml;
using System.Xml.Linq;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates
{
    internal class QueryFile
    {
        public IEnumerable<string> SearchPaths { get; set; }
        public FilePattern Pattern { get; set; }

        public QueryFile(IEnumerable<string> paths, FilePattern pattern)
        {
            SearchPaths = new List<string>(paths);
            Pattern = pattern;
        }

        public static QueryFile Load(string path)
        {
            return Load(File.OpenRead(path));
        }

        public static QueryFile Load(Stream stream)
        {
            try
            {
                XDocument doc = XDocument.Load(stream);
                XElement root = doc.Element("query");
                if (root == null) throw new XmlException("This file is not a valid saved search");

                List<string> paths = new List<string>();

                XElement pathsNode = root.Element("paths");
                IEnumerable<XElement> pathNodeList;
                if (paths == null || !(pathNodeList = pathsNode.Elements("path")).Any())
                    throw new XmlException("There are no paths listed in this query file");

                foreach (XElement pathNode in pathNodeList)
                {
                    string uri = pathNode.Attribute("uri")?.Value;
                    if (uri != null)
                    {
                        paths.Add(uri);
                    }
                }

                XElement patternsNode = root.Element("patterns");
                IEnumerable<XElement> patternNodeList;
                if (patternsNode == null || !(patternNodeList = patternsNode.Elements("pattern")).Any())
                    throw new XmlException("There are no patterns to match in this query file");

                List<string> masks = new List<string>();

                foreach (XElement patternNode in patternNodeList)
                {
                    string mask = patternNode.Attribute("mask")?.Value;
                    if (mask != null)
                    {
                        masks.Add(mask);
                    }
                }

                FilePattern pattern = new FilePattern(patternsNode.Attribute("description")?.Value, masks);
                return new QueryFile(paths, pattern);
            }
            finally
            {
                stream?.Dispose();
            }
        }

        public void Save(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            Save(File.Open(path, FileMode.Create));
        }

        public void Save(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            try
            {
                XElement root = new XElement("query");

                XElement paths = new XElement("paths");
                foreach (string path in SearchPaths)
                {
                    XElement pathNode = new XElement("path");
                    pathNode.SetAttributeValue("uri", path);
                    paths.Add(pathNode);
                }
                root.Add(paths);

                XElement patterns = new XElement("patterns");
                patterns.SetAttributeValue("description", Pattern.Description);
                foreach (string pattern in Pattern.Subpatterns)
                {
                    XElement patternNode = new XElement("pattern");
                    patternNode.SetAttributeValue("mask", pattern);
                    patterns.Add(patternNode);
                }
                root.Add(patterns);

                XDocument doc = new XDocument();
                doc.Add(root);
                doc.Save(stream);
            }
            finally
            {
                stream?.Dispose();
            }
        }
    }
}
