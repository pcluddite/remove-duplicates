//
//    Remove Duplicates
//    Copyright (C) Timothy Baxendale
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
using System.Threading;
using System.Threading.Tasks;
using Baxendale.Data.Collections.Concurrent;

namespace Baxendale.RemoveDuplicates.Search
{
    internal class DuplicateFinder
    {
        public FilePattern Pattern { get; set; }
        public bool IncludeSubdirectories { get; set; }

        public event EventHandler<DirectorySearchEventArgs> OnBeginDirectorySearch;
        public event EventHandler<DirectorySearchEventArgs> OnEndDirectorySearch;
        public event EventHandler<NewFileFoundEventArgs> OnNewFileFound;
        public event EventHandler<DuplicateFoundEventArgs> OnFoundDuplicate;
        public event EventHandler<SearchCompletedEventArgs> OnSearchCompleted;

        private IDictionary<string, DirectoryInfo> _searchedDirs = new ConcurrentDictionary<string, DirectoryInfo>();

        public DuplicateFinder(FilePattern pattern)
        {
            Pattern = pattern;
        }

        public IEnumerable<UniqueFile> Search(IEnumerable<string> directoryPaths)
        {
            return Search(directoryPaths.Select(path => new DirectoryInfo(path)));
        }

        public IEnumerable<UniqueFile> Search(IEnumerable<DirectoryInfo> directories)
        {
            Task<IEnumerable<UniqueFile>> searchTask = SearchAsync(directories);
            searchTask.Wait();
            return searchTask.Result;
        }

        public async Task<IEnumerable<UniqueFile>> SearchAsync(IEnumerable<string> directoryPaths)
        {
            return await SearchAsync(directoryPaths, CancellationToken.None);
        }

        public async Task<IEnumerable<UniqueFile>> SearchAsync(IEnumerable<string> directoryPaths, CancellationToken cancellationToken)
        {
            return await SearchAsync(directoryPaths.Select(path => new DirectoryInfo(path)), cancellationToken);
        }

        public async Task<IEnumerable<UniqueFile>> SearchAsync(IEnumerable<DirectoryInfo> directories)
        {
            return await SearchAsync(directories, CancellationToken.None);
        }

        public async Task<IEnumerable<UniqueFile>> SearchAsync(IEnumerable<DirectoryInfo> directories, CancellationToken cancellationToken)
        {
            SearchSettings settings = new SearchSettings(Pattern ?? FilePattern.AllFiles, IncludeSubdirectories);
            List<UniqueFile> duplicates = new List<UniqueFile>();

            foreach (DirectoryInfo directory in directories) {
                if (_searchedDirs.TryAdd(directory.FullName, directory))
                    duplicates.AddRange(await SearchAsync(directory, settings, cancellationToken));
            }

            OnSearchCompleted?.Invoke(this, new SearchCompletedEventArgs(duplicates));
            return duplicates;
        }

        private async Task<IEnumerable<UniqueFile>> SearchAsync(DirectoryInfo dirMetaData, SearchSettings settings, CancellationToken cancellationToken)
        {
            Queue<DirectoryInfo> directories = new Queue<DirectoryInfo>();
            directories.Enqueue(dirMetaData);

            List<UniqueFile> duplicates = new List<UniqueFile>();

            do {
                if (cancellationToken.IsCancellationRequested)
                    break;

                dirMetaData = directories.Dequeue();

                if (settings.IncludeSubdirectories) {
                    foreach (DirectoryInfo subDirectory in dirMetaData.GetDirectories()) {
                        if (_searchedDirs.TryAdd(subDirectory.FullName, subDirectory))
                            directories.Enqueue(subDirectory);
                    }
                }

                duplicates.AddRange(await ScanFilesAsync(dirMetaData, settings, cancellationToken));
            }
            while (directories.Count > 0);

            return duplicates;
        }

        private async Task<IEnumerable<UniqueFile>> ScanFilesAsync(DirectoryInfo dirMetaData, SearchSettings settings, CancellationToken cancellationToken)
        {
            return await Task.Run(() => ScanFiles(dirMetaData, settings, cancellationToken));
        }

        private IEnumerable<UniqueFile> ScanFiles(DirectoryInfo dirMetaData, SearchSettings settings, CancellationToken cancellationToken)
        {
            OnBeginDirectorySearch?.Invoke(this, new DirectorySearchEventArgs(dirMetaData));

            HashSet<FileInfo> files = new HashSet<FileInfo>();

            foreach (string subpattern in settings.Pattern)
                files.UnionWith(dirMetaData.GetFiles(subpattern));

            List<UniqueFile> foundDupes = new List<UniqueFile>(files.Count);

            foreach (FileInfo fileMetaData in files) {
                // ignore blank files
                if (fileMetaData.Length == 0)
                    continue;

                UniqueFile uniqueFile;
                Md5Hash checksum;

                using (FileStream stream = fileMetaData.OpenRead()) {
                    // ignore empty files up to 1 KB
                    if (stream.IsEmpty(Sizes.KB_SIZE))
                        continue;
                    checksum = Md5Hash.ComputeHash(stream);
                }

                if (cancellationToken.IsCancellationRequested)
                    break;

                if (settings.AllFiles.TryGetValue(checksum, out uniqueFile)) {
                    if (uniqueFile.ContainsPath(fileMetaData.FullName))
                        continue;

                    bool cancelled = false;
                    if (OnFoundDuplicate != null) {
                        DuplicateFoundEventArgs foundArgs = new DuplicateFoundEventArgs(new UniqueFile(uniqueFile), fileMetaData);
                        OnFoundDuplicate(this, foundArgs);
                        cancelled = foundArgs.Cancel;
                    }
                    if (!cancelled) {
                        uniqueFile.Add(fileMetaData);
                        foundDupes.Add(uniqueFile);
                    }
                }
                else {
                    uniqueFile = new UniqueFile(fileMetaData, checksum);
                    settings.AllFiles[checksum] = uniqueFile;
                    OnNewFileFound?.Invoke(this, new NewFileFoundEventArgs(uniqueFile, fileMetaData));
                }
            }

            OnEndDirectorySearch?.Invoke(this, new DirectorySearchEventArgs(dirMetaData, foundDupes.ToArray()));

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

        private readonly struct SearchSettings
        {
            public ConcurrentDictionary<Md5Hash, UniqueFile> AllFiles { get; }
            public FilePattern Pattern { get; }
            public bool IncludeSubdirectories { get; }

            public SearchSettings(FilePattern pattern, bool subdirs = true)
            {
                AllFiles = new ConcurrentDictionary<Md5Hash, UniqueFile>();
                Pattern = pattern;
                IncludeSubdirectories = subdirs;
            }
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
