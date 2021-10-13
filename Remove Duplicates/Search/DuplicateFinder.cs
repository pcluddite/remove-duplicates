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
        public FilePattern Pattern { get; set; }

        public event EventHandler<DirectorySearchEventArgs> OnBeginDirectorySearch;
        public event EventHandler<NewFileFoundEventArgs> OnNewFileFound;
        public event EventHandler<DuplicateFoundEventArgs> OnFoundDuplicate;
        public event EventHandler<DirectorySearchEventArgs> OnEndDirectorySearch;
        public event EventHandler<SearchCompletedEventArgs> OnSearchCompleted;

        public DuplicateFinder(FilePattern pattern)
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
            FilePattern pattern = Pattern ?? FilePattern.AllFiles;
            ConcurrentDictionary<Md5Hash, UniqueFile> uniqueFiles = new ConcurrentDictionary<Md5Hash, UniqueFile>();
            ConcurrentDictionary<Md5Hash, UniqueFile> duplicatedFiles = new ConcurrentDictionary<Md5Hash, UniqueFile>();
            List<Task> searchTasks = new List<Task>();
            foreach (string subpattern in pattern)
            {
                foreach (DirectoryInfo directory in directories)
                    searchTasks.Add(SearchAsync(directory, subpattern, uniqueFiles, duplicatedFiles));
            }
            Task.WaitAll(searchTasks.ToArray());
            OnSearchCompleted?.Invoke(this, new SearchCompletedEventArgs(duplicatedFiles.Values));
            return duplicatedFiles.Values;
        }

        private async Task SearchAsync(DirectoryInfo dirMetaData, string pattern, ConcurrentDictionary<Md5Hash, UniqueFile> allFiles, ConcurrentDictionary<Md5Hash, UniqueFile> duplicatedFiles)
        {
            await Task.Run(() => Search(dirMetaData, pattern, allFiles, duplicatedFiles));
        }

        private void Search(DirectoryInfo dirMetaData, string pattern, ConcurrentDictionary<Md5Hash, UniqueFile> allFiles, ConcurrentDictionary<Md5Hash, UniqueFile> duplicatedFiles)
        {
            Queue<DirectoryInfo> directories = new Queue<DirectoryInfo>();
            directories.Enqueue(dirMetaData);
            List<Task<List<UniqueFile>>> searchTasks = new List<Task<List<UniqueFile>>>();
            do
            {
                dirMetaData = directories.Dequeue();
                if (OnBeginDirectorySearch != null)
                {
                    DirectorySearchEventArgs args = new DirectorySearchEventArgs(dirMetaData);
                    OnBeginDirectorySearch.Invoke(this, args);
                    dirMetaData = args.Directory;
                }

                foreach (DirectoryInfo subDirectory in dirMetaData.GetDirectories())
                    directories.Enqueue(subDirectory);
                
                Task<List<UniqueFile>> searchTask = ScanFilesAsync(dirMetaData, pattern, allFiles, duplicatedFiles);
                searchTask.ContinueWith((Task<List<UniqueFile>> task) => OnEndDirectorySearch?.Invoke(this, new DirectorySearchEventArgs(dirMetaData, task.Result.ToArray())));
                searchTasks.Add(searchTask);
            }
            while (directories.Count > 0);
            Task.WaitAll(searchTasks.ToArray());
        }

        private async Task<List<UniqueFile>> ScanFilesAsync(DirectoryInfo dirMetaData, string pattern, ConcurrentDictionary<Md5Hash, UniqueFile> allFiles, ConcurrentDictionary<Md5Hash, UniqueFile> duplicatedFiles)
        {
            return await Task.Run(() => ScanFiles(dirMetaData, pattern, allFiles, duplicatedFiles));
        }

        private List<UniqueFile> ScanFiles(DirectoryInfo dirMetaData, string pattern, ConcurrentDictionary<Md5Hash, UniqueFile> allFiles, ConcurrentDictionary<Md5Hash, UniqueFile> duplicatedFiles)
        { 
            FileInfo[] files = dirMetaData.GetFiles(pattern);
            List<UniqueFile> foundDupes = new List<UniqueFile>(files.Length);

            foreach (FileInfo fileMetaData in files)
            {
                UniqueFile uniqueFile;
                Md5Hash checksum = Md5Hash.ComputeHash(fileMetaData.OpenRead());

                if (allFiles.TryGetValue(checksum, out uniqueFile) && !uniqueFile.ContainsPath(fileMetaData.FullName))
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
                    duplicatedFiles[checksum] = uniqueFile;
                }
                else
                {
                    uniqueFile = new UniqueFile(fileMetaData, checksum);
                    allFiles[checksum] = uniqueFile;
                    OnNewFileFound?.Invoke(this, new NewFileFoundEventArgs(uniqueFile, fileMetaData));
                }
            }
            return foundDupes;
        }

        public static IEnumerable<UniqueFile> FindDuplicates(IEnumerable<string> searchPaths, FilePattern pattern)
        {
            DuplicateFinder finder = new DuplicateFinder(pattern);
            return finder.Search(searchPaths);
        }

        public static IEnumerable<UniqueFile> FindDuplicates(IEnumerable<DirectoryInfo> searchDirectories, FilePattern pattern)
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
