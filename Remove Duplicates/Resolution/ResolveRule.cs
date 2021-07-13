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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Baxendale.Data.Collections;
using Baxendale.Data.Xml;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates.Resolution
{
    internal class ResolveRule : ICollection<IFileComparer>
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

        public IEnumerable<IFileComparer> Rules => _rules.AsReadOnly();

        public int Count => _rules.Count;

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

        public IEnumerable<FileResolution> Resolve(IEnumerable<UniqueFile> files)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileInfo> Resolve(UniqueFile duplicates)
        {
            return Resolve(duplicates.Paths.Select(p => new FileInfo(p)));
        }

        public IEnumerable<FileInfo> Resolve(IEnumerable<FileInfo> duplicates)
        {
            CompositeComparer<FileInfo> comparer = new CompositeComparer<FileInfo>(_rules);
            using (IEnumerator<FileInfo> e = duplicates.OrderBy(x => x, comparer).GetEnumerator())
            {
                FileInfo last = null;
                if (e.MoveNext())
                    yield return last = e.Current;
                while (e.MoveNext() && comparer.Compare(e.Current, last) == 0)
                    yield return last = e.Current;
            }
        }

        public void Clear()
        {
            _rules.Clear();
        }

        public bool Contains(IFileComparer item)
        {
            return _rules.Contains(item);
        }

        public void CopyTo(IFileComparer[] array, int arrayIndex)
        {
            _rules.CopyTo(array, arrayIndex);
        }

        bool ICollection<IFileComparer>.IsReadOnly => false;

        public IEnumerator<IFileComparer> GetEnumerator()
        {
            foreach (IComparer<FieldInfo> comparer in _rules)
                yield return (IFileComparer)comparer;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
