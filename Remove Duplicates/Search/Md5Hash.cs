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
        private const int CHAR_BIT = 8; // bits in a byte
        private const int HEX_RADIX = 16; // hex base
        private const int LONG_BYTES = sizeof(long); // bytes in long
        private const int LLONG_BYTES = LONG_BYTES * 2; // bytes in a double long (heareafter "long long" or "llong")

        private const int LONG_STR16_SIZE = LONG_BYTES * 2;
        private const int LLONG_STR16_SIZE = LLONG_BYTES * 2;

        private static readonly MD5 md5 = MD5.Create();
        private static readonly object _syncRoot = new object();

        private readonly long _part1;
        private readonly long _part2;

        public unsafe byte[] Bytes
        {
            get
            {
                byte[] bytes = new byte[LLONG_BYTES];
                fixed (byte* lpBytes = bytes)
                    GetBytes(lpBytes, _part1, _part2);
                return bytes;
            }
        }

        public unsafe string Base16
        {
            get
            {
                char* lpBuff = stackalloc char[LLONG_STR16_SIZE];
                byte* lpBytes = stackalloc byte[LLONG_BYTES];

                GetBytes(lpBytes, _part1, _part2);
                BytesToString(lpBuff, 0, lpBytes, LLONG_BYTES, HEX_RADIX);

                return new string(lpBuff, 0, LLONG_STR16_SIZE);
            }
        }

        public string Base64
        {
            get
            {
                return Convert.ToBase64String(Bytes);
            }
        }

        private unsafe Md5Hash(byte* lpHash)
        {
            _part1 = 0;
            _part2 = 0;
            SetBytes(lpHash, ref _part1, ref _part2);
        }

        private Md5Hash(long part1, long part2)
        {
            _part1 = part1;
            _part2 = part2;
        }

        public unsafe static Md5Hash ComputeHash(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            fixed(byte* lpHash = LockComputeHash(data))
                return new Md5Hash(lpHash);
        }

        public unsafe static Md5Hash ComputeHash(Stream byteStream)
        {
            if (byteStream == null) throw new ArgumentNullException(nameof(byteStream));
            fixed (byte* lpHash = LockComputeHash(byteStream))
                return new Md5Hash(lpHash);
        }

        private static byte[] LockComputeHash(byte[] data)
        {
            lock (_syncRoot) return md5.ComputeHash(data);
        }

        private static byte[] LockComputeHash(Stream byteStream)
        {
            lock (_syncRoot) return md5.ComputeHash(byteStream);
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

        public unsafe static Md5Hash FromXml(XAttribute attribute)
        {
            string base16hash = attribute.Value;
            if (base16hash == null || base16hash.Length != LLONG_STR16_SIZE)
                throw new XmlSerializationException(attribute, "String is not the correct size to be an MD5 hash.");

            byte* hashBytes = stackalloc byte[LLONG_BYTES];

            int cIdx = 0;
            int len = base16hash.Length;

            do
            {
                uint u = GetByte(base16hash[cIdx++]) * (uint)HEX_RADIX;
                u += GetByte(base16hash[cIdx++]);
                *hashBytes++ = (byte)u;
            }
            while (cIdx < len);

            return new Md5Hash(hashBytes);
        }

        private static unsafe void BytesToString(char* buffer, int buffStart, byte* data, int count, int radix)
        {
            int buffIdx = buffStart;
            for (int datIdx = 0; datIdx < count; ++datIdx)
            {
                byte b = data[datIdx];
                uint div = b / (uint)radix;
                uint mod = b % (uint)radix;
                buffer[buffIdx++] = GetHexChar(div);
                buffer[buffIdx++] = GetHexChar(mod);
            }
        }

        private static unsafe void GetBytes(byte* lpBuff, long part1, long part2)
        {
            *((long*)lpBuff) = part1;
            *((long*)(lpBuff + LONG_BYTES)) = part2;
        }

        private static unsafe void SetBytes(byte* lpBytes, ref long part1, ref long part2)
        {
            part1 = *((long*)lpBytes);
            part2 = *((long*)(lpBytes + LONG_BYTES));
        }

        private static char GetHexChar(uint n)
        {
            return (char)(n < 10 ? '0' + n : 'a' + (n % 10));
        }

        public static byte GetByte(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return (byte)(c - '0');
            }
            else if (c >= 'a' && c <= 'f')
            {
                return (byte)(c - 'a' + 10);
            }
            throw new FormatException($"Unexpected character in hexadecimal string '{c}'.");
        }
    }
}
