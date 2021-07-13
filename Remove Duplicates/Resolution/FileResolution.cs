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
using System.Xml.Linq;
using Baxendale.Data.Xml;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates.Resolution
{
    internal class FileResolution : IXmlSerializableObject
    {
        public Md5Hash Hash { get; }
        public IEnumerable<FileInfo> Originals { get; }
        public IEnumerable<FileInfo> Duplicates { get; }

        public FileResolution(Md5Hash hash, IEnumerable<FileInfo> originals, IEnumerable<FileInfo> duplicates)
        {
            Originals = originals.ToArray();
            Duplicates = duplicates.ToArray();
            Hash = hash;
        }

        public XElement ToXml(XName name)
        {
            XElement content = new XElement(name);
            content.Add(XmlSerializer.Default.Serialize(Hash, "hash"));
            content.Add(new XElement("originals", 
                            from fileInfo in Originals
                            let attr = new XAttribute("path", fileInfo.FullName)
                            select new XElement("file", attr)));
            content.Add(new XElement("duplicates",
                            from fileInfo in Duplicates
                            let attr = new XAttribute("path", fileInfo.FullName)
                            select new XElement("file", attr)));
            return content;
        }

        public static FileResolution FromXml(XElement content)
        {
            Md5Hash hash = XmlSerializer.Default.Deserialize<Md5Hash>(content.Attribute("hash"));
            IEnumerable<FileInfo> originals = from file in content.Elements("file")
                                              let path = file.Attribute("path")
                                              where path != null
                                              select new FileInfo(path.Value);
            IEnumerable<FileInfo> duplicates = from file in content.Elements("file")
                                               let path = file.Attribute("path")
                                               where path != null
                                               select new FileInfo(path.Value);
            return new FileResolution(hash, originals, duplicates);
        }
    }
}
