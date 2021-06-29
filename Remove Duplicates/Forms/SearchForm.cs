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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates.Forms
{
    internal partial class SearchForm : Form
    {
        private int filesSearched = 0;
        private int duplicatesFound = 0;
        private int numberOfDots = 0;

        public IEnumerable<string> SearchPaths { get; set; }
        public FilePattern Pattern { get; set; }
        internal Task<IEnumerable<UniqueFile>> SearchTask { get; private set; }

        private Action<UniqueFile, FileInfo> UpdateFoundDuplicateDelegate;
        private Action<UniqueFile, FileInfo> UpdateNewFileFoundDelegate;

        private DateTime StartTime { get; set; }

        public SearchForm(IEnumerable<string> paths, FilePattern pattern)
        {
            SearchPaths = paths;
            Pattern = pattern;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            Disable();
            StartTime = DateTime.Now;
            SearchTask = StartSearch(SearchPaths, Pattern);
            base.OnLoad(e);
        }

        private void dotTimer_Tick(object sender, EventArgs e)
        {
            Text = "Remove Duplicates | Searching" + "".PadRight(++numberOfDots % 4, '.');
        }

        private Task<IEnumerable<UniqueFile>> StartSearch(IEnumerable<string> paths, FilePattern pattern)
        {
            duplicatesFound = 0;
            numberOfDots = 0;

            UpdateFoundDuplicateDelegate = new Action<UniqueFile, FileInfo>(UpdateFoundDuplicate);
            UpdateNewFileFoundDelegate = new Action<UniqueFile, FileInfo>(UpdateNewFileFound);

            DuplicateFinder finder = new DuplicateFinder(pattern);
            finder.OnFoundDuplicate += Finder_OnFoundDuplicate;
            finder.OnNewFileFound += Finder_OnNewFileFound;
            finder.OnSearchCompleted += Finder_OnSearchCompleted;
            return finder.SearchAsync(paths);
        }

        private void Finder_OnNewFileFound(object sender, DuplicateFinder.NewFileFoundEventArgs e)
        {
            BeginInvoke(UpdateNewFileFoundDelegate, e.NewFile, e.FileMetaData);
        }

        private void Finder_OnFoundDuplicate(object sender, DuplicateFinder.DuplicateFoundEventArgs e)
        {
            BeginInvoke(UpdateFoundDuplicateDelegate, e.Duplicate, e.DuplicateFileMetaData);
        }

        private void UpdateNewFileFound(UniqueFile file, FileInfo fileMetaData)
        {
            toolStripStatusLabelDirectory.Text = fileMetaData.FullName;
            IncrementFilesSearched();
        }

        private void UpdateFoundDuplicate(UniqueFile file, FileInfo fileMetaData)
        {
            string hash = file.Hash.Base16;
            ListViewGroup duplicateGroup = lstViewResults.Groups[hash];
            ListViewItem item;
            if (duplicateGroup == null)
            {
                duplicateGroup = new ListViewGroup(hash, hash);
                lstViewResults.Groups.Add(duplicateGroup);
                foreach(string path in file.Paths)
                {
                    item = new ListViewItem(path, duplicateGroup);
                    lstViewResults.Items.Add(item);
                    item.EnsureVisible();
                }
            }
            item = new ListViewItem(fileMetaData.FullName, duplicateGroup);
            lstViewResults.Items.Add(item);
            duplicateGroup.Header = $"{duplicateGroup.Items.Count} Files, {fileMetaData.Length.FormatAsSize(1)} per file";
            item.EnsureVisible();
            IncrementDuplicatesFound();
            IncrementFilesSearched();
            toolStripStatusLabelDirectory.Text = fileMetaData.FullName;
        }

        private void Finder_OnSearchCompleted(object sender, DuplicateFinder.SearchCompletedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate { Enable(); }));
        }

        private void Enable()
        {
            dotTimer.Stop();
            Text = "Remove Duplicates | Results";
            toolStripProgressBar.Visible = false;
            toolStripStatusLabelDirectory.Text = "Completed in " + (DateTime.Now - StartTime).ToString(@"h\:mm\:ss");
        }

        private void Disable()
        {
            toolStripProgressBar.Visible = true;
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

        private void lstViewResults_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            ClearOpenToolStripMenu();
            ClearShowInExplorer();

            openToolStripMenuItem.Text = "&Open";
            showInExplorerToolStripMenuItem.Text = "Show in E&xplorer";

            if (lstViewResults.SelectedItems.Count > 1)
            {
                resolveToolStripMenuItem.Text = "&Resolve Duplicates";
                BuildMultiItemRightClickMenu();
            }
            else if (lstViewResults.SelectedItems.Count == 1)
            {
                resolveToolStripMenuItem.Text = "&Resolve Duplicate";
                BuildSingleItemRightClickMenu();
            }
            else
            {
                openToolStripMenuItem.Visible = false;
                showInExplorerToolStripMenuItem.Visible = false;
                toolStripSeparator.Visible = false;
                resolveToolStripMenuItem.Enabled = false;
            }
            rightClickMenu.Show(lstViewResults, e.Location);
        }

        private void BuildSingleItemRightClickMenu()
        {
            ListViewItem selItem = lstViewResults.SelectedItems[0];
            showInExplorerToolStripMenuItem.Tag = selItem.Text;
            openToolStripMenuItem.Tag = selItem.Text;
            openToolStripMenuItem.Visible = true;
            showInExplorerToolStripMenuItem.Visible = true;
            toolStripSeparator.Visible = true;
            resolveToolStripMenuItem.Enabled = true;
        }

        private void BuildMultiItemRightClickMenu()
        {
            var selItems = lstViewResults.SelectedItems.Cast<ListViewItem>();
            var dirs = selItems.Select(o => Path.GetDirectoryName(o.Text)).ToSet();

            if (dirs.Count == 1)
            {
                openToolStripMenuItem.Text = "&Open Directory";
                showInExplorerToolStripMenuItem.Text = "Show Directory in E&xplorer";
                openToolStripMenuItem.Tag = dirs.First();
                showInExplorerToolStripMenuItem.Tag = dirs.First();
            }
            else
            {
                foreach (string path in dirs.OrderBy(path => path, StringComparer.CurrentCultureIgnoreCase))
                {
                    ToolStripMenuItem openItem = new ToolStripMenuItem(path);
                    openItem.Tag = path;
                    openItem.Click += OpenToolStripItem_Click;
                    openToolStripMenuItem.DropDownItems.Add(openItem);

                    ToolStripMenuItem explorerItem = new ToolStripMenuItem(path);
                    explorerItem.Tag = path;
                    explorerItem.Click += ExplorerToolStripItem_Click;
                    showInExplorerToolStripMenuItem.DropDownItems.Add(explorerItem);
                }
            }
            openToolStripMenuItem.Visible = true;
            showInExplorerToolStripMenuItem.Visible = true;
            toolStripSeparator.Visible = true;
            resolveToolStripMenuItem.Enabled = true;
        }

        private void ClearOpenToolStripMenu()
        {
            foreach (ToolStripItem item in openToolStripMenuItem.DropDownItems)
                item.Click -= OpenToolStripItem_Click;
            openToolStripMenuItem.DropDownItems.Clear();
        }

        private void ClearShowInExplorer()
        {
            foreach (ToolStripItem item in showInExplorerToolStripMenuItem.DropDownItems)
                item.Click -= ExplorerToolStripItem_Click;
            showInExplorerToolStripMenuItem.DropDownItems.Clear();
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
                Process.Start((string)item.Tag);
            }
            catch (Exception ex) when (ex is Win32Exception || ex is IOException)
            {
                Program.ShowError(this, ex);
            }
        }

        private void lstViewResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lstViewResults.Sorting = (lstViewResults.Sorting == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
        }

        private void lstViewResults_Resize(object sender, EventArgs e)
        {
            pathColumnHeader.Width = lstViewResults.Width - 30;
        }

        private void StatusBar_TextUpdated(object sender, EventArgs e)
        {
            MinimumSize = new Size((toolStripProgressBar.Visible ? toolStripProgressBar.Width : 0) +
                toolStripStatusFilesCount.Width + toolStripStatusDuplicatesCount.Width + toolStripStatusLabelDirectory.Width,
                MinimumSize.Height);
        }
    }
}
