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

namespace Baxendale.RemoveDuplicates.Search
{
    internal struct Md5Hash : IEquatable<Md5Hash>
    {
        private static readonly MD5 md5 = MD5.Create();
        private static readonly object _object = new object();

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

        public static Md5Hash ComputeHash(byte[] data)
        {
            return new Md5Hash(md5.ComputeHash(data));
        }

        public static Md5Hash ComputeHash(Stream byteStream)
        {
            try
            {
                byte[] hashBytes;
                lock (_object)
                {
                    hashBytes = md5.ComputeHash(byteStream);
                }
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
    }
}
