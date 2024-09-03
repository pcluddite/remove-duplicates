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
namespace Baxendale.RemoveDuplicates.Forms
{
    partial class ResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pathColumnHeader = new System.Windows.Forms.ColumnHeader();
            statusStrip = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusFilesCount = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusDuplicatesCount = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelDirectory = new System.Windows.Forms.ToolStripStatusLabel();
            lstViewResults = new System.Windows.Forms.ListView();
            rightClickMenu = new System.Windows.Forms.ContextMenuStrip(components);
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            showInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            recycleFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            deleteAllInFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            recycleAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            saveResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dotTimer = new System.Windows.Forms.Timer(components);
            saveResultsFileDialog = new System.Windows.Forms.SaveFileDialog();
            statusStrip.SuspendLayout();
            rightClickMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pathColumnHeader
            // 
            pathColumnHeader.Text = "Paths";
            pathColumnHeader.Width = 497;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusFilesCount, toolStripStatusDuplicatesCount, toolStripStatusLabelDirectory });
            statusStrip.Location = new System.Drawing.Point(0, 509);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip.Size = new System.Drawing.Size(772, 24);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new System.Drawing.Size(117, 18);
            toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            toolStripProgressBar.VisibleChanged += StatusBar_TextUpdated;
            // 
            // toolStripStatusFilesCount
            // 
            toolStripStatusFilesCount.Name = "toolStripStatusFilesCount";
            toolStripStatusFilesCount.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            toolStripStatusFilesCount.Size = new System.Drawing.Size(90, 19);
            toolStripStatusFilesCount.Text = "0 Files Searched";
            toolStripStatusFilesCount.TextChanged += StatusBar_TextUpdated;
            // 
            // toolStripStatusDuplicatesCount
            // 
            toolStripStatusDuplicatesCount.Name = "toolStripStatusDuplicatesCount";
            toolStripStatusDuplicatesCount.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            toolStripStatusDuplicatesCount.Size = new System.Drawing.Size(108, 19);
            toolStripStatusDuplicatesCount.Text = "0 Duplicates Found";
            toolStripStatusDuplicatesCount.TextChanged += StatusBar_TextUpdated;
            // 
            // toolStripStatusLabelDirectory
            // 
            toolStripStatusLabelDirectory.Name = "toolStripStatusLabelDirectory";
            toolStripStatusLabelDirectory.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            toolStripStatusLabelDirectory.Size = new System.Drawing.Size(438, 19);
            toolStripStatusLabelDirectory.Spring = true;
            toolStripStatusLabelDirectory.Text = "C:\\Test\\Subdir";
            // 
            // lstViewResults
            // 
            lstViewResults.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lstViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { pathColumnHeader });
            lstViewResults.ContextMenuStrip = rightClickMenu;
            lstViewResults.FullRowSelect = true;
            lstViewResults.Location = new System.Drawing.Point(14, 14);
            lstViewResults.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lstViewResults.Name = "lstViewResults";
            lstViewResults.Size = new System.Drawing.Size(742, 489);
            lstViewResults.TabIndex = 1;
            lstViewResults.UseCompatibleStateImageBehavior = false;
            lstViewResults.View = System.Windows.Forms.View.Details;
            lstViewResults.ColumnClick += lstViewResults_ColumnClick;
            lstViewResults.MouseClick += lstViewResults_MouseClick;
            lstViewResults.Resize += lstViewResults_Resize;
            // 
            // rightClickMenu
            // 
            rightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { openToolStripMenuItem, showInExplorerToolStripMenuItem, deleteToolStripMenuItem, toolStripSeparator1, deleteAllInFolderToolStripMenuItem, toolStripSeparator2, saveResultsToolStripMenuItem });
            rightClickMenu.Name = "rightClickItemMenu";
            rightClickMenu.Size = new System.Drawing.Size(232, 126);
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += OpenToolStripItem_Click;
            // 
            // showInExplorerToolStripMenuItem
            // 
            showInExplorerToolStripMenuItem.Name = "showInExplorerToolStripMenuItem";
            showInExplorerToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            showInExplorerToolStripMenuItem.Text = "Show in Explorer";
            showInExplorerToolStripMenuItem.Click += ExplorerToolStripItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { recycleFileToolStripMenuItem, deleteFileToolStripMenuItem });
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            deleteToolStripMenuItem.Text = "Delete";
            // 
            // recycleFileToolStripMenuItem
            // 
            recycleFileToolStripMenuItem.Name = "recycleFileToolStripMenuItem";
            recycleFileToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            recycleFileToolStripMenuItem.Text = "Recycle";
            recycleFileToolStripMenuItem.Click += RecycleToolStripMenuItem_Click;
            // 
            // deleteFileToolStripMenuItem
            // 
            deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            deleteFileToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            deleteFileToolStripMenuItem.Text = "Delete (Cannot be undone!)";
            deleteFileToolStripMenuItem.Click += DeleteFileToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(228, 6);
            // 
            // deleteAllInFolderToolStripMenuItem
            // 
            deleteAllInFolderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { recycleAllToolStripMenuItem, deleteAllToolStripMenuItem });
            deleteAllInFolderToolStripMenuItem.Name = "deleteAllInFolderToolStripMenuItem";
            deleteAllInFolderToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            deleteAllInFolderToolStripMenuItem.Text = "Delete All Duplicates in Folder";
            // 
            // recycleAllToolStripMenuItem
            // 
            recycleAllToolStripMenuItem.Name = "recycleAllToolStripMenuItem";
            recycleAllToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            recycleAllToolStripMenuItem.Text = "Recycle";
            // 
            // deleteAllToolStripMenuItem
            // 
            deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            deleteAllToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            deleteAllToolStripMenuItem.Text = "Delete (Cannot be undone!)";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(228, 6);
            // 
            // saveResultsToolStripMenuItem
            // 
            saveResultsToolStripMenuItem.Enabled = false;
            saveResultsToolStripMenuItem.Name = "saveResultsToolStripMenuItem";
            saveResultsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            saveResultsToolStripMenuItem.Text = "Save Results";
            saveResultsToolStripMenuItem.Click += saveResultsToolStripMenuItem_Click;
            // 
            // dotTimer
            // 
            dotTimer.Interval = 1000;
            dotTimer.Tick += dotTimer_Tick;
            // 
            // saveResultsFileDialog
            // 
            saveResultsFileDialog.FileName = "*.xml";
            saveResultsFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            // 
            // ResultForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(772, 533);
            Controls.Add(lstViewResults);
            Controls.Add(statusStrip);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(264, 284);
            Name = "ResultForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Searching";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            rightClickMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDirectory;
        private System.Windows.Forms.ListView lstViewResults;
        private System.Windows.Forms.Timer dotTimer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDuplicatesCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusFilesCount;
        private System.Windows.Forms.ContextMenuStrip rightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader pathColumnHeader;
        private System.Windows.Forms.ToolStripMenuItem saveResultsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveResultsFileDialog;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recycleFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllInFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recycleAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}