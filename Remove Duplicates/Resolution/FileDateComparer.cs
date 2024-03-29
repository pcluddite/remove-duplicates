﻿//
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
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Baxendale.Data.Xml;
using Baxendale.Serialization;

namespace Baxendale.RemoveDuplicates.Resolution
{
    internal abstract class FileDateComparer : IFileComparer
    {
        [XmlSerializableProperty(Name = "reverse",  SerializeDefault = false)]
        public virtual bool Reverse { get; set; }

        [XmlSerializableProperty(Name = "type")]
        public virtual string ComparerTypeName
        {
            get
            {
                string typeName = GetType().Name;
                typeName = typeName.Remove(typeName.Length - nameof(FileDateComparer).Length);
                return char.ToLower(typeName[0]) + typeName.Substring(1);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        protected abstract DateTime GetDate(FileInfo file);
        
        public virtual int Compare(FileInfo x, FileInfo y)
        {
            return GetDate(x).CompareTo(GetDate(y));
        }

        public static FileDateComparer FromXml(XElement node)
        {
            string typeName = node.Attribute("type")?.Value;
            if (typeName == null)
                throw new SerializationException(node, "File date comparer type is not set");
            typeName = char.ToUpper(typeName[0]) + typeName.Substring(1);
            Type comparerType = Type.GetType($"{typeof(FileDateComparer).Namespace}.{typeName}{nameof(FileDateComparer)}");
            if (comparerType == null)
                throw new SerializationException(node, $"The file comparer type '{typeName}' is not recognized");
            FileDateComparer comparer = (FileDateComparer)Activator.CreateInstance(comparerType);
            comparer.Reverse = bool.TrueString.EqualsIgnoreCase(node.Attribute("reverse")?.Value);
            return comparer;
        }
    }

    internal class LastModifiedFileDateComparer : FileDateComparer
    {
        protected override DateTime GetDate(FileInfo file)
        {
            return file.LastWriteTimeUtc;
        }
    }

    internal class LastAccessFileDateComparer : FileDateComparer
    {
        protected override DateTime GetDate(FileInfo file)
        {
            return file.LastAccessTimeUtc;
        }
    }

    internal class CreationFileDateComparer : FileDateComparer
    {
        protected override DateTime GetDate(FileInfo file)
        {
            return file.CreationTimeUtc;
        }
    }
}
