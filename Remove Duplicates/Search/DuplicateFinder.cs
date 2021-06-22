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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Baxendale.RemoveDuplicates.Search
{
    internal class DuplicateFinder
    {
        public string Pattern { get; set; }

        public event EventHandler<DirectorySearchEventArgs> OnBeginDirectorySearch;
        public event EventHandler<NewFileFoundEventArgs> OnNewFileFound;
        public event EventHandler<DuplicateFoundEventArgs> OnFoundDuplicate;
        public event EventHandler<DirectorySearchEventArgs> OnEndDirectorySearch;
        public event EventHandler<SearchCompletedEventArgs> OnSearchCompleted;

        public DuplicateFinder(string pattern)
        {
            Pattern = pattern;
        }

        public async Task<IEnumerable<UniqueFile>> SearchAsync(IEnumerable<string> directoryPaths)
        {
            return await Task.Run(() => Search(directoryPaths));
        }

        public async Task<IEnumerable<UniqueFile>> SearchAsync(IEnumerable<DirectoryInfo> directories)
        {
            return await Task.Run(() => Search(directories));
        }

        public IEnumerable<UniqueFile> Search(IEnumerable<string> directoryPaths)
        {
            return Search(directoryPaths.Select(path => new DirectoryInfo(path)));
        }

        public IEnumerable<UniqueFile> Search(IEnumerable<DirectoryInfo> directories)
        {
            ConcurrentDictionary<Md5Hash, UniqueFile> uniqueFiles = new ConcurrentDictionary<Md5Hash, UniqueFile>();
            string fullPatern = Pattern;

            if (string.IsNullOrEmpty(fullPatern))
                fullPatern = FilePattern.AllFiles.FullPattern;

            List<Task> searchTasks = new List<Task>();
            foreach (string pattern in fullPatern.Split(';'))
            {
                foreach (DirectoryInfo directory in directories)
                    searchTasks.Add(RecursiveSearchAsync(directory, pattern, uniqueFiles));
            }
            Task.WaitAll(searchTasks.ToArray());
            IEnumerable<UniqueFile> duplicates = uniqueFiles.Values.Where(o => o.Paths.Count > 1);
            OnSearchCompleted?.Invoke(this, new SearchCompletedEventArgs(duplicates));
            return duplicates;
        }

        private async Task RecursiveSearchAsync(DirectoryInfo dirMetaData, string pattern, ConcurrentDictionary<Md5Hash, UniqueFile> fileDictionary)
        {
            await Task.Run(() => RecursiveSearch(dirMetaData, pattern, fileDictionary));
        }

        private void RecursiveSearch(DirectoryInfo dirMetaData, string pattern, ConcurrentDictionary<Md5Hash, UniqueFile> fileDictionary)
        {
            DirectorySearchEventArgs args = new DirectorySearchEventArgs(dirMetaData);
            OnBeginDirectorySearch?.Invoke(this, args);
            dirMetaData = args.Directory;

            List<UniqueFile> foundDupes = SearchFiles(dirMetaData, pattern, fileDictionary);

            OnEndDirectorySearch?.Invoke(this, new DirectorySearchEventArgs(dirMetaData, foundDupes.ToArray()));

            DirectoryInfo[] subDirs = dirMetaData.GetDirectories();
            if (subDirs.Length > 0)
            {
                Task[] searchTasks = new Task[subDirs.Length];
                int n = 0;
                foreach (DirectoryInfo dirInfo in subDirs)
                    searchTasks[n++] = RecursiveSearchAsync(dirInfo, pattern, fileDictionary);
                Task.WaitAll(searchTasks);
            }
        }

        private List<UniqueFile> SearchFiles(DirectoryInfo dirMetaData, string pattern, ConcurrentDictionary<Md5Hash, UniqueFile> fileDictionary)
        { 
            FileInfo[] files = dirMetaData.GetFiles(pattern);
            List<UniqueFile> foundDupes = new List<UniqueFile>(files.Length);

            foreach (FileInfo fileMetaData in files)
            {
                UniqueFile uniqueFile;
                Md5Hash checksum = Md5Hash.ComputeHash(fileMetaData.OpenRead());

                if (fileDictionary.TryGetValue(checksum, out uniqueFile) && !uniqueFile.ContainsPath(fileMetaData.FullName))
                {
                    bool cancelled = false;
                    if (OnFoundDuplicate != null)
                    {
                        DuplicateFoundEventArgs foundArgs = new DuplicateFoundEventArgs(new UniqueFile(uniqueFile), fileMetaData);
                        OnFoundDuplicate(this, foundArgs);
                        cancelled = foundArgs.Cancel;
                    }
                    if (!cancelled)
                    {
                        uniqueFile.Add(fileMetaData);
                        foundDupes.Add(uniqueFile);
                    }
                }
                else
                {
                    uniqueFile = new UniqueFile(fileMetaData, checksum);
                    fileDictionary[checksum] = uniqueFile;
                    OnNewFileFound?.Invoke(this, new NewFileFoundEventArgs(uniqueFile, fileMetaData));
                }
            }
            return foundDupes;
        }

        public static IEnumerable<UniqueFile> FindDuplicates(IEnumerable<string> searchPaths, string pattern)
        {
            DuplicateFinder finder = new DuplicateFinder(pattern);
            return finder.Search(searchPaths);
        }

        public static IEnumerable<UniqueFile> FindDuplicates(IEnumerable<DirectoryInfo> searchDirectories, string pattern)
        {
            DuplicateFinder finder = new DuplicateFinder(pattern);
            return finder.Search(searchDirectories);
        }

        public class DirectorySearchEventArgs : EventArgs
        {
            public DirectoryInfo Directory { get; set; }
            public ICollection<UniqueFile> FoundDuplicates { get; }

            public DirectorySearchEventArgs(DirectoryInfo directory)
                : this(directory, null)
            {
            }

            public DirectorySearchEventArgs(DirectoryInfo directory, ICollection<UniqueFile> foundDuplicates)
            {
                Directory = directory;
                FoundDuplicates = foundDuplicates;
            }
        }

        public class DuplicateFoundEventArgs : EventArgs
        {
            public FileInfo DuplicateFileMetaData { get; }
            public UniqueFile Duplicate { get; }
            public bool Cancel { get; set; }

            public DuplicateFoundEventArgs(UniqueFile duplicate, FileInfo info)
            {
                DuplicateFileMetaData = info;
                Duplicate = duplicate;
                Cancel = false;
            }
        }

        public class NewFileFoundEventArgs : EventArgs
        {
            public FileInfo FileMetaData { get; }
            public UniqueFile NewFile { get; }

            public NewFileFoundEventArgs(UniqueFile file, FileInfo info)
            {
                NewFile = file;
                FileMetaData = info;
            }
        }

        public class SearchCompletedEventArgs : EventArgs
        {
            public IEnumerable<UniqueFile> Duplicates { get; }

            public SearchCompletedEventArgs(IEnumerable<UniqueFile> duplicates)
            {
                Duplicates = duplicates;
            }
        }
    }
}
