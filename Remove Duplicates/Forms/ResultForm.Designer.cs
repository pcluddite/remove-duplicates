﻿//
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
            this.components = new System.ComponentModel.Container();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pathColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusFilesCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusDuplicatesCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDirectory = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstViewResults = new System.Windows.Forms.ListView();
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.resolveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dotTimer = new System.Windows.Forms.Timer(this.components);
            this.saveResultsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip.SuspendLayout();
            this.rightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // pathColumnHeader
            // 
            this.pathColumnHeader.Text = "Paths";
            this.pathColumnHeader.Width = 497;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusFilesCount,
            this.toolStripStatusDuplicatesCount,
            this.toolStripStatusLabelDirectory});
            this.statusStrip.Location = new System.Drawing.Point(0, 440);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(662, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar.VisibleChanged += new System.EventHandler(this.StatusBar_TextUpdated);
            // 
            // toolStripStatusFilesCount
            // 
            this.toolStripStatusFilesCount.Name = "toolStripStatusFilesCount";
            this.toolStripStatusFilesCount.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusFilesCount.Size = new System.Drawing.Size(90, 17);
            this.toolStripStatusFilesCount.Text = "0 Files Searched";
            this.toolStripStatusFilesCount.TextChanged += new System.EventHandler(this.StatusBar_TextUpdated);
            // 
            // toolStripStatusDuplicatesCount
            // 
            this.toolStripStatusDuplicatesCount.Name = "toolStripStatusDuplicatesCount";
            this.toolStripStatusDuplicatesCount.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusDuplicatesCount.Size = new System.Drawing.Size(108, 17);
            this.toolStripStatusDuplicatesCount.Text = "0 Duplicates Found";
            this.toolStripStatusDuplicatesCount.TextChanged += new System.EventHandler(this.StatusBar_TextUpdated);
            // 
            // toolStripStatusLabelDirectory
            // 
            this.toolStripStatusLabelDirectory.Name = "toolStripStatusLabelDirectory";
            this.toolStripStatusLabelDirectory.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabelDirectory.Size = new System.Drawing.Size(347, 17);
            this.toolStripStatusLabelDirectory.Spring = true;
            this.toolStripStatusLabelDirectory.Text = "C:\\Test\\Subdir";
            // 
            // lstViewResults
            // 
            this.lstViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pathColumnHeader});
            this.lstViewResults.ContextMenuStrip = this.rightClickMenu;
            this.lstViewResults.FullRowSelect = true;
            this.lstViewResults.HideSelection = false;
            this.lstViewResults.Location = new System.Drawing.Point(12, 12);
            this.lstViewResults.Name = "lstViewResults";
            this.lstViewResults.Size = new System.Drawing.Size(637, 424);
            this.lstViewResults.TabIndex = 1;
            this.lstViewResults.UseCompatibleStateImageBehavior = false;
            this.lstViewResults.View = System.Windows.Forms.View.Details;
            this.lstViewResults.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstViewResults_ColumnClick);
            this.lstViewResults.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstViewResults_MouseClick);
            this.lstViewResults.Resize += new System.EventHandler(this.lstViewResults_Resize);
            // 
            // rightClickMenu
            // 
            this.rightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.showInExplorerToolStripMenuItem,
            this.toolStripSeparator,
            this.resolveToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveResultsToolStripMenuItem});
            this.rightClickMenu.Name = "rightClickItemMenu";
            this.rightClickMenu.Size = new System.Drawing.Size(173, 104);
            this.rightClickMenu.Opening += new System.ComponentModel.CancelEventHandler(this.rightClickMenu_Opening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripItem_Click);
            // 
            // showInExplorerToolStripMenuItem
            // 
            this.showInExplorerToolStripMenuItem.Name = "showInExplorerToolStripMenuItem";
            this.showInExplorerToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showInExplorerToolStripMenuItem.Text = "Show in Explorer";
            this.showInExplorerToolStripMenuItem.Click += new System.EventHandler(this.ExplorerToolStripItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(169, 6);
            // 
            // resolveToolStripMenuItem
            // 
            this.resolveToolStripMenuItem.Name = "resolveToolStripMenuItem";
            this.resolveToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.resolveToolStripMenuItem.Text = "&Resolve Duplicates";
            // 
            // saveResultsToolStripMenuItem
            // 
            this.saveResultsToolStripMenuItem.Enabled = false;
            this.saveResultsToolStripMenuItem.Name = "saveResultsToolStripMenuItem";
            this.saveResultsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.saveResultsToolStripMenuItem.Text = "Save Results";
            this.saveResultsToolStripMenuItem.Click += new System.EventHandler(this.saveResultsToolStripMenuItem_Click);
            // 
            // dotTimer
            // 
            this.dotTimer.Interval = 1000;
            this.dotTimer.Tick += new System.EventHandler(this.dotTimer_Tick);
            // 
            // saveResultsFileDialog
            // 
            this.saveResultsFileDialog.FileName = "*.xml";
            this.saveResultsFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 462);
            this.Controls.Add(this.lstViewResults);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(229, 251);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Searching";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.rightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem resolveToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader pathColumnHeader;
        private System.Windows.Forms.ToolStripMenuItem saveResultsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveResultsFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}