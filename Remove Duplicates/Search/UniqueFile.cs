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

namespace Baxendale.RemoveDuplicates.Search
{
    internal class UniqueFile : IEquatable<UniqueFile>
    {
        private readonly object _object = new object();

        private HashSet<string> _filePaths;
        private Md5Hash _checksum;

        public Md5Hash Hash
        {
            get
            {
                return _checksum;
            }
        }

        public ICollection<string> Paths
        {
            get
            {
                lock (_object)
                {
                    return new ReadOnlyCollection<string>(_filePaths);
                }
            }
        }

        public long FileSize { get; }

        public UniqueFile(UniqueFile other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            lock (_object)
            lock (other._object)
            {
                _filePaths = new HashSet<string>(other._filePaths);
                _checksum = other._checksum;
                FileSize = other.FileSize;
            }
        }

        public UniqueFile(string path)
            : this(new FileInfo(path))
        {
        }

        public UniqueFile(FileInfo fileInfo)
            : this(fileInfo, Md5Hash.ComputeHash(fileInfo.OpenRead()))
        {
        }

        public UniqueFile(FileInfo fileInfo, Md5Hash checksum)
            : this(new HashSet<string>(), checksum, fileInfo.Length)
        {
            lock (_object)
            {
                _filePaths.Add(fileInfo.FullName);
            }
        }

        private UniqueFile(HashSet<string> filePaths, Md5Hash checksum, long size)
        {
            _checksum = checksum;
            _filePaths = filePaths;
            FileSize = size;
        }

        public bool Add(string path)
        {
            lock (_object)
            {
                return _filePaths.Add(Path.GetFullPath(path));
            }
        }

        public bool Add(FileInfo fileInfo)
        {
            lock (_object)
            {
                return _filePaths.Add(fileInfo.FullName);
            }
        }

        public bool Add(UniqueFile file)
        {
            if (!Equals(file))
                return false;
            lock (_object)
            lock (file._object)
            {
                _filePaths.UnionWith(file._filePaths);
                return true;
            }
        }

        public bool ContainsPath(string fullName)
        {
            lock (_object)
            {
                return _filePaths.Contains(fullName);
            }
        }


        public bool Remove(string path)
        {
            lock (_object)
            {
                return Paths.Remove(Path.GetFullPath(path));
            }
        }

        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            UniqueFile file = obj as UniqueFile;
            if (file != null)
                return Equals(file);
            FileInfo metaData = obj as FileInfo;
            if (metaData == null)
                return false;
            return Equals(metaData);
        }

        public bool Equals(UniqueFile other)
        {
            if (other == null)
                return false;
            if (other == this)
                return true;

            lock (_object)
            lock (other._object)
            {
                return FileSize == other.FileSize && _checksum == other.Hash;
            }
        }

        public bool Equals(FileInfo metaData)
        {
            if (metaData == null) throw new ArgumentNullException(nameof(metaData));
            // check file size first since we can determine inequality quickly

            lock (_object)
            {
                return metaData.Length == FileSize && Hash == Md5Hash.ComputeHash(metaData.OpenRead());
            }
        }

        public override string ToString()
        {
            return $"{Hash.Base16} {Paths.FirstOrDefault()}";
        }

        public void TrimExcess()
        {
            lock (_object)
            {
                _filePaths.TrimExcess();
            }
        }
    }
}
