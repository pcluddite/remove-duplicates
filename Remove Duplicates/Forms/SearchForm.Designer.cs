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
namespace Baxendale.RemoveDuplicates.Forms
{
    partial class SearchForm
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
            System.Windows.Forms.ColumnHeader columnHeader2;
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.Windows.Forms.ColumnHeader columnHeader3;
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusFilesCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusDuplicatesCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDirectory = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstViewResults = new System.Windows.Forms.ListView();
            this.dotTimer = new System.Windows.Forms.Timer(this.components);
            columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "MD5";
            columnHeader2.Width = 214;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Count";
            columnHeader1.Width = 51;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Paths";
            columnHeader3.Width = 497;
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
            this.statusStrip.Size = new System.Drawing.Size(668, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // toolStripStatusFilesCount
            // 
            this.toolStripStatusFilesCount.Name = "toolStripStatusFilesCount";
            this.toolStripStatusFilesCount.Size = new System.Drawing.Size(90, 17);
            this.toolStripStatusFilesCount.Text = "0 Files Searched";
            // 
            // toolStripStatusDuplicatesCount
            // 
            this.toolStripStatusDuplicatesCount.Name = "toolStripStatusDuplicatesCount";
            this.toolStripStatusDuplicatesCount.Size = new System.Drawing.Size(108, 17);
            this.toolStripStatusDuplicatesCount.Text = "0 Duplicates Found";
            // 
            // toolStripStatusLabelDirectory
            // 
            this.toolStripStatusLabelDirectory.Name = "toolStripStatusLabelDirectory";
            this.toolStripStatusLabelDirectory.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabelDirectory.Size = new System.Drawing.Size(322, 17);
            this.toolStripStatusLabelDirectory.Spring = true;
            this.toolStripStatusLabelDirectory.Text = "C:\\Test\\Subdir";
            // 
            // lstViewResults
            // 
            this.lstViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3});
            this.lstViewResults.FullRowSelect = true;
            this.lstViewResults.Location = new System.Drawing.Point(12, 12);
            this.lstViewResults.Name = "lstViewResults";
            this.lstViewResults.Size = new System.Drawing.Size(644, 425);
            this.lstViewResults.TabIndex = 1;
            this.lstViewResults.UseCompatibleStateImageBehavior = false;
            this.lstViewResults.View = System.Windows.Forms.View.Details;
            // 
            // dotTimer
            // 
            this.dotTimer.Interval = 1000;
            this.dotTimer.Tick += new System.EventHandler(this.dotTimer_Tick);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 462);
            this.Controls.Add(this.lstViewResults);
            this.Controls.Add(this.statusStrip);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Searching";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
    }
}