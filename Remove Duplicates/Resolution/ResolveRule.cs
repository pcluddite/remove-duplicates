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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Baxendale.DataManagement.Collections;
using Baxendale.DataManagement.Xml;

namespace Baxendale.RemoveDuplicates.Resolution
{
    internal class ResolveRule : IXmlSerializableObject, IEnumerable<IFileComparer>
    {
        static ResolveRule()
        {
            IEnumerable<TypeInfo> fileComparers = Assembly.GetExecutingAssembly().DefinedTypes
                .Where(type => type.Namespace == typeof(ResolveRule).Namespace
                    && type.IsClass && !type.IsAbstract 
                    && typeof(IFileComparer).IsAssignableFrom(type));
            MethodInfo registerMethod = typeof(XmlSerializer).GetMethod(nameof(XmlSerializer.RegisterType), BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder, CallingConventions.Standard, new[] { typeof(XName) }, Collections<ParameterModifier>.EmptyArray);
            foreach (TypeInfo typeInfo in fileComparers)
            {
                MethodInfo typeRegisterMethod = registerMethod.MakeGenericMethod(typeInfo);
                typeRegisterMethod.Invoke(null, new [] { (XName)typeInfo.Name });
            }
        }

        private readonly List<IFileComparer> _rules;

        public ICollection<IFileComparer> Rules => _rules.AsReadOnly();

        public ResolveRule()
        {
            _rules = new List<IFileComparer>();
        }

        public ResolveRule(IEnumerable<IFileComparer> rules)
        {
            _rules = new List<IFileComparer>(rules);
        }

        public void Add(IFileComparer comparer)
        {
            _rules.Add(comparer);
        }

        public bool Remove(IFileComparer comparer)
        {
            return _rules.Remove(comparer);
        }

        public void RemoveAt(int index)
        {
            _rules.RemoveAt(index);
        }

        public XObject ToXml(XName name)
        {
            return XmlSerializer.Serialize(_rules, name);
        }

        public static ResolveRule FromXml(XElement node, XName name)
        {
            IEnumerable<IFileComparer> rules = node.Elements().Select(n => (IFileComparer)XmlSerializer.Deserialize(n));
            return new ResolveRule(rules);
        }

        public IEnumerator<IFileComparer> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
