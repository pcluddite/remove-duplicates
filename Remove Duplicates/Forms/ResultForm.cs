//
//    Remove Duplicates
//    Copyright (C) 2021-2024 Timothy Baxendale
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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Baxendale.Data.Xml;
using Baxendale.RemoveDuplicates.Search;
using Baxendale.Serialization;

using Microsoft.VisualBasic.FileIO;

namespace Baxendale.RemoveDuplicates.Forms
{
    internal partial class ResultForm : Form
    {
        private int filesSearched = 0;
        private int duplicatesFound = 0;
        private int numberOfDots = 0;

        public IEnumerable<string> SearchPaths { get; set; }
        public FilePattern Pattern { get; set; }
        public bool IncludeSubdirectories { get; set; }
        public Task<IEnumerable<UniqueFile>> SearchTask { get; private set; }

        private Action<UniqueFile, FileInfo> UpdateFoundDuplicateDelegate;
        private Action<UniqueFile, FileInfo> UpdateNewFileFoundDelegate;
        private Action<DirectoryInfo> UpdateDirectorySearchDelegate;

        private DateTime StartTime { get; set; }

        public bool LiveUpdates { get; set; }

        private bool _liveUpdates;
        private Dictionary<Md5Hash, ListViewGroup> _groups;
        private List<ListViewItem> _results;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public ResultForm(IEnumerable<string> paths, FilePattern pattern, bool subdirs = true)
        {
            SearchPaths = paths;
            Pattern = pattern;
            IncludeSubdirectories = subdirs;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            SearchTask = StartSearch(SearchPaths, Pattern);
            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!SearchTask.IsCompleted && !_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource.Cancel();
            base.OnFormClosing(e);
        }

        private void DotTimer_Tick(object sender, EventArgs e)
        {
            Text = "Remove Duplicates | Searching" + "".PadRight(++numberOfDots % 4, '.');
        }

        private Task<IEnumerable<UniqueFile>> StartSearch(IEnumerable<string> paths, FilePattern pattern)
        {
            _liveUpdates = LiveUpdates;
            duplicatesFound = 0;
            numberOfDots = 0;
            StartTime = DateTime.Now;

            toolStripStatusLabelDirectory.Text = "Initializing Search...";
            _groups = new Dictionary<Md5Hash, ListViewGroup>();
            _results = new List<ListViewItem>();

            Disable();

            DuplicateFinder finder = new DuplicateFinder(pattern)
            {
                IncludeSubdirectories = IncludeSubdirectories
            };

            UpdateFoundDuplicateDelegate = new Action<UniqueFile, FileInfo>(UpdateFoundDuplicate);
            UpdateNewFileFoundDelegate = new Action<UniqueFile, FileInfo>(UpdateNewFileFound);
            UpdateDirectorySearchDelegate = new Action<DirectoryInfo>(UpdateDirectorySearch);

            finder.OnNewFileFound += Finder_OnNewFileFound;
            finder.OnFoundDuplicate += Finder_OnFoundDuplicate;
            finder.OnBeginDirectorySearch += Finder_OnBeginDirectorySearch;
            finder.OnSearchCompleted += Finder_OnSearchCompleted;

            return finder.SearchAsync(paths, _cancellationTokenSource.Token);
        }

        private void Finder_OnEndDirectorySearch(object sender, DuplicateFinder.DirectorySearchEventArgs e)
        {
            BeginInvoke(UpdateDirectorySearchDelegate, e.Directory);
        }

        private void Finder_OnBeginDirectorySearch(object sender, DuplicateFinder.DirectorySearchEventArgs e)
        {
            BeginInvoke(UpdateDirectorySearchDelegate, e.Directory);
        }

        private void Finder_OnNewFileFound(object sender, DuplicateFinder.NewFileFoundEventArgs e)
        {
            BeginInvoke(UpdateNewFileFoundDelegate, e.NewFile, e.FileMetaData);
        }

        private void Finder_OnFoundDuplicate(object sender, DuplicateFinder.DuplicateFoundEventArgs e)
        {
            BeginInvoke(UpdateFoundDuplicateDelegate, e.Duplicate, e.DuplicateFileMetaData);
        }

        private void UpdateDirectorySearch(DirectoryInfo directoryInfo)
        {
            toolStripStatusLabelDirectory.Text = directoryInfo.FullName;
        }

        private void UpdateNewFileFound(UniqueFile file, FileInfo fileMetaData)
        {
            IncrementFilesSearched();
        }

        private void UpdateFoundDuplicate(UniqueFile file, FileInfo fileMetaData)
        {
            ListViewItem item = AddDuplicateToList(file, fileMetaData);
            if (_liveUpdates)
                item.EnsureVisible();
            IncrementDuplicatesFound();
            IncrementFilesSearched();
        }

        private ListViewGroup FindResultGroup(Md5Hash hash)
        {
            if (_liveUpdates)
                return lstViewResults.Groups[hash.Base16];
            ListViewGroup group;
            if (_groups.TryGetValue(hash, out group))
                return group;
            return null;
        }

        private void AddListViewResult(ListViewItem item)
        {
            if (_liveUpdates)
            {
                lstViewResults.Items.Add(item);
            }
            else
            {
                _results.Add(item);
            }
        }

        private ListViewItem AddDuplicateToList(UniqueFile file, FileInfo fileMetaData)
        {
            ListViewGroup duplicateGroup = FindResultGroup(file.Hash);
            ListViewItem item;
            if (duplicateGroup == null)
            {
                duplicateGroup = new ListViewGroup(file.Hash.Base16, "");
                if (_liveUpdates)
                {
                    lstViewResults.Groups.Add(duplicateGroup);
                }
                else
                {
                    _groups[file.Hash] = duplicateGroup;
                }
                duplicateGroup.Tag = file;
                foreach (string path in file.Paths)
                {
                    item = new ListViewItem(path, duplicateGroup);
                    AddListViewResult(item);
                }
            }
            item = new ListViewItem(fileMetaData.FullName, duplicateGroup);
            AddListViewResult(item);
            ((UniqueFile)duplicateGroup.Tag).Add(fileMetaData);
            duplicateGroup.Header = $"{duplicateGroup.Items.Count} files, {file.FileSize.FormatAsSize(decimals: 1)} per file";
            return item;
        }

        private void Finder_OnSearchCompleted(object sender, DuplicateFinder.SearchCompletedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate { Enable(); }));
        }

        private void Enable()
        {
            dotTimer.Stop();

            if (!_liveUpdates)
            {
                toolStripStatusLabelDirectory.Text = "Updating Results View...";
                lstViewResults.Groups.AddRange(_groups.Values.ToArray());
                lstViewResults.Items.AddRange(_results.ToArray());
                lstViewResults.Enabled = true;
                _results.Clear();
                _groups.Clear();
                _results.TrimExcess();
            }

            Text = "Remove Duplicates | Results";

            toolStripProgressBar.Visible = false;
            saveResultsToolStripMenuItem.Enabled = true;

            long totalDupSize = 0;

            foreach (ListViewGroup group in lstViewResults.Groups)
            {
                long size = ((UniqueFile)group.Tag).FileSize;
                int dupCount = group.Items.Count - 1;
                totalDupSize += (size * dupCount);
            }
            string verb = duplicatesFound == 1 ? "duplicate is" : "duplicates are";
            toolStripStatusDuplicatesCount.Text = $"{duplicatesFound:N0} {verb} taking up {totalDupSize.FormatAsSize()}";
            toolStripStatusLabelDirectory.Text = $"Completed in {DateTime.Now - StartTime:h\\:mm\\:ss}";
            StatusBar_TextUpdated(this, EventArgs.Empty);
        }

        private void Disable()
        {
            saveResultsToolStripMenuItem.Enabled = false;
            toolStripProgressBar.Visible = true;
            lstViewResults.Enabled = _liveUpdates;
            dotTimer.Start();
        }

        private void IncrementDuplicatesFound()
        {
            if (duplicatesFound == 1)
            {
                toolStripStatusDuplicatesCount.Text = $"{++duplicatesFound:N0} Duplicate Found";
            }
            else
            {
                toolStripStatusDuplicatesCount.Text = $"{++duplicatesFound:N0} Duplicates Found";
            }
        }

        private void IncrementFilesSearched()
        {
            if (filesSearched == 1)
            {
                toolStripStatusFilesCount.Text = $"{++filesSearched:N0} File Searched";
            }
            else
            {
                toolStripStatusFilesCount.Text = $"{++filesSearched:N0} Files Searched";
            }
        }

        private void LstViewResults_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (lstViewResults.SelectedItems.Count > 1)
            {
                openToolStripMenuItem.Visible = false;
                showInExplorerToolStripMenuItem.Visible = false;

                deleteToolStripMenuItem.Visible = false;
                toolStripSeparator1.Visible = false;

                deleteAllInFolderToolStripMenuItem.Text = string.Empty;
                deleteAllInFolderToolStripMenuItem.Visible = false;
                toolStripSeparator2.Visible = false;
            }
            else if (lstViewResults.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = lstViewResults.SelectedItems[0];
                showInExplorerToolStripMenuItem.Tag = selectedItem.Text;
                openToolStripMenuItem.Tag = selectedItem.Text;

                openToolStripMenuItem.Visible = true;
                showInExplorerToolStripMenuItem.Visible = true;

                deleteToolStripMenuItem.Visible = true;
                toolStripSeparator1.Visible = true;

                deleteAllInFolderToolStripMenuItem.Text = "Delete all duplicates in " + Path.GetDirectoryName(selectedItem.Text);
                deleteAllInFolderToolStripMenuItem.Visible = true;
                toolStripSeparator2.Visible = true;
            }
            else
            {
                openToolStripMenuItem.Visible = false;
                showInExplorerToolStripMenuItem.Visible = false;

                deleteToolStripMenuItem.Visible = false;
                toolStripSeparator1.Visible = false;

                deleteAllInFolderToolStripMenuItem.Text = string.Empty;
                deleteAllInFolderToolStripMenuItem.Visible = false;
                toolStripSeparator2.Visible = false;
            }

            rightClickMenu.Show(lstViewResults, e.Location);
        }

        private void ExplorerToolStripItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.HasDropDownItems) return;
            try
            {
                Process.Start("explorer.exe", $"/select,\"{(string)item.Tag}\"");
            }
            catch (Exception ex) when (ex is Win32Exception || ex is IOException)
            {
                Program.ShowError(this, ex);
            }
        }

        private void OpenToolStripItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.HasDropDownItems) return;
            try
            {
                Process.Start(new ProcessStartInfo((string)item.Tag) { UseShellExecute = true });
            }
            catch (Exception ex) when (ex is Win32Exception || ex is IOException)
            {
                Program.ShowError(this, ex);
            }
        }

        private void LstViewResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lstViewResults.Sorting = (lstViewResults.Sorting == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
        }

        private void LstViewResults_Resize(object sender, EventArgs e)
        {
            pathColumnHeader.Width = lstViewResults.Width - 30;
        }

        private void StatusBar_TextUpdated(object sender, EventArgs e)
        {
            MinimumSize = new Size((toolStripProgressBar.Visible ? toolStripProgressBar.Width : 0) +
                toolStripStatusFilesCount.Width + toolStripStatusDuplicatesCount.Width + 50,
                MinimumSize.Height);
        }

        private void SaveResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveResultsFileDialog.ShowDialog() == DialogResult.OK)
            {
                IEnumerable<UniqueFile> files = lstViewResults.Groups.Cast<ListViewGroup>().Select(i => i.Tag).Cast<UniqueFile>();
                SearchResult results = new SearchResult(new Query(SearchPaths, Pattern), files);
                try
                {
                    XmlSerializer.Default.Save(results, saveResultsFileDialog.FileName);
                    Program.ShowInfo(this, "Results saved");
                }
                catch (Exception ex) when (ex is IOException || ex is SerializationException || ex is ArgumentException)
                {
                    Program.ShowError(this, ex.Message);
                }
            }
        }

        private void RecycleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string files = lstViewResults.SelectedItems.Count > 1 ? "these files" : "this file";
            if (Program.ShowDialog(this, $"Are you sure you want to recycle {files}?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                List<ListViewItem> deleted = new List<ListViewItem>();
                foreach (ListViewItem item in lstViewResults.SelectedItems)
                {
                    try
                    {
#if !DEBUG
                        FileSystem.DeleteFile(item.Text, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
#endif
                        deleted.Add(item);
                    }
                    catch (Exception ex) when (ex is IOException || ex is SecurityException || ex is UnauthorizedAccessException)
                    {
                        Program.ShowError(this, ex);
                    }
                }
                RemoveListViewItems(deleted);
            }
        }

        private void DeleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string files = lstViewResults.SelectedItems.Count > 1 ? "these files" : "this file";
            if (Program.ShowDialog(this, $"Are you sure you want to delete {files}?\nThis operation cannot be undone!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                List<ListViewItem> deleted = new List<ListViewItem>();
                foreach (ListViewItem item in lstViewResults.SelectedItems)
                {
                    try
                    {
#if !DEBUG
                        File.Delete(item.Text);
#endif
                        deleted.Add(item);
                    }
                    catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
                    {
                        Program.ShowError(this, ex);
                    }
                }
                RemoveListViewItems(deleted);
            }
        }

        private void RemoveListViewItems(IEnumerable<ListViewItem> items)
        {
            foreach (ListViewItem item in items)
                RemoveListViewItem(item);
        }

        private void RemoveListViewItem(ListViewItem item)
        {
            ListViewGroup group = item.Group;
            UniqueFile file = (UniqueFile)group.Tag;

            file.Remove(item.Text);
            lstViewResults.Items.Remove(item);

            if (group.Items.Count < 2)
            {
                lstViewResults.Items.Remove(group.Items[0]);
                lstViewResults.Groups.Remove(group);
            }
        }
    }
}
