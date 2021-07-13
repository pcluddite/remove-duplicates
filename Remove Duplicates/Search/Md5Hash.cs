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
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;
using Baxendale.Data.Xml;

namespace Baxendale.RemoveDuplicates.Search
{
    internal struct Md5Hash : IEquatable<Md5Hash>, IXmlSerializableObject
    {
        private const int INT64_STR16_SIZE = sizeof(long) * 2;
        private const int INT128_STR16_SIZE = INT64_STR16_SIZE * 2;

        private const string INVALID_HASH_MSG = "Invalid Md5 Hash";

        private static readonly MD5 md5 = MD5.Create();
        private static readonly object _syncRoot = new object();

        private long _part1;
        private long _part2;

        public unsafe byte[] Bytes
        {
            get
            {
                byte[] bytes = new byte[sizeof(long) * 2];
                fixed (byte* lpBytes = bytes)
                {
                    *((long*)lpBytes) = _part1;
                    *((long*)(lpBytes + sizeof(long))) = _part2;
                }
                return bytes;
            }
        }

        public string Base16
        {
            get
            {
                return Convert.ToString(_part1, 16) + Convert.ToString(_part2, 16);
            }
        }

        public string Base64
        {
            get
            {
                return Convert.ToBase64String(Bytes);
            }
        }

        private unsafe Md5Hash(byte[] hash)
        {
            fixed (byte* lpBytes = hash)
            {
                _part1 = *((long*)lpBytes);
                _part2 = *((long*)(lpBytes + sizeof(long)));
            }
        }

        private Md5Hash(long part1, long part2)
        {
            _part1 = part1;
            _part2 = part2;
        }

        public static Md5Hash ComputeHash(byte[] data)
        {
            return new Md5Hash(md5.ComputeHash(data));
        }

        public static Md5Hash ComputeHash(Stream byteStream)
        {
            try
            {
                byte[] hashBytes;
                lock (_syncRoot)
                    hashBytes = md5.ComputeHash(byteStream);
                return new Md5Hash(hashBytes);
            }
            finally
            {
                byteStream?.Dispose();
            }
        }

        public override bool Equals(object obj)
        {
            Md5Hash? other = obj as Md5Hash?;
            if (other == null)
                return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return _part1.GetHashCode() ^ _part2.GetHashCode();
        }

        public bool Equals(Md5Hash other)
        {
            return _part1 == other._part1 && _part2 == other._part2;
        }

        public static bool operator ==(Md5Hash left, Md5Hash right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Md5Hash left, Md5Hash right)
        {
            return !left.Equals(right);
        }

        public XAttribute ToXml(XName name)
        {
            return new XAttribute(name, Base16);
        }

        public static Md5Hash FromXml(XAttribute attribute)
        {
            string base16hash = attribute.Value;
            if (base16hash == null || base16hash.Length != INT128_STR16_SIZE)
                throw new XmlSerializationException(INVALID_HASH_MSG);
            long part1, part2;
            try
            {
                part1 = Convert.ToInt64(base16hash.Substring(0, INT64_STR16_SIZE), 16);
                part2 = Convert.ToInt64(base16hash.Substring(INT64_STR16_SIZE, INT64_STR16_SIZE), 16);
            }
            catch(Exception ex) when (ex is FormatException || ex is OverflowException)
            {
                throw new XmlSerializationException($"{INVALID_HASH_MSG}. {ex.Message}");
            }
            return new Md5Hash(part1, part2);
        }
    }
}
