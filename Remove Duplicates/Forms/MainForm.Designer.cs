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
    partial class MainForm
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
            this.lstPaths = new System.Windows.Forms.ListBox();
            this.grpPaths = new System.Windows.Forms.GroupBox();
            this.btnRemovePath = new System.Windows.Forms.Button();
            this.btnAddPath = new System.Windows.Forms.Button();
            this.grpBoxPattern = new System.Windows.Forms.GroupBox();
            this.comboBoxPatterns = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openQueryFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveQueryFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.grpPaths.SuspendLayout();
            this.grpBoxPattern.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstPaths
            // 
            this.lstPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPaths.FormattingEnabled = true;
            this.lstPaths.ItemHeight = 16;
            this.lstPaths.Location = new System.Drawing.Point(5, 21);
            this.lstPaths.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstPaths.Name = "lstPaths";
            this.lstPaths.Size = new System.Drawing.Size(473, 116);
            this.lstPaths.TabIndex = 0;
            this.lstPaths.SelectedIndexChanged += new System.EventHandler(this.lstPaths_SelectedIndexChanged);
            // 
            // grpPaths
            // 
            this.grpPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPaths.Controls.Add(this.btnRemovePath);
            this.grpPaths.Controls.Add(this.btnAddPath);
            this.grpPaths.Controls.Add(this.lstPaths);
            this.grpPaths.Location = new System.Drawing.Point(12, 12);
            this.grpPaths.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPaths.Name = "grpPaths";
            this.grpPaths.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPaths.Size = new System.Drawing.Size(485, 177);
            this.grpPaths.TabIndex = 1;
            this.grpPaths.TabStop = false;
            this.grpPaths.Text = "Search Paths";
            // 
            // btnRemovePath
            // 
            this.btnRemovePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemovePath.Enabled = false;
            this.btnRemovePath.Location = new System.Drawing.Point(324, 143);
            this.btnRemovePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRemovePath.Name = "btnRemovePath";
            this.btnRemovePath.Size = new System.Drawing.Size(75, 23);
            this.btnRemovePath.TabIndex = 2;
            this.btnRemovePath.Text = "Re&move";
            this.btnRemovePath.UseVisualStyleBackColor = true;
            this.btnRemovePath.Click += new System.EventHandler(this.btnRemovePath_Click);
            // 
            // btnAddPath
            // 
            this.btnAddPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPath.Location = new System.Drawing.Point(404, 143);
            this.btnAddPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddPath.Name = "btnAddPath";
            this.btnAddPath.Size = new System.Drawing.Size(75, 23);
            this.btnAddPath.TabIndex = 1;
            this.btnAddPath.Text = "&Add";
            this.btnAddPath.UseVisualStyleBackColor = true;
            this.btnAddPath.Click += new System.EventHandler(this.btnAddPath_Click);
            // 
            // grpBoxPattern
            // 
            this.grpBoxPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxPattern.Controls.Add(this.comboBoxPatterns);
            this.grpBoxPattern.Location = new System.Drawing.Point(12, 194);
            this.grpBoxPattern.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpBoxPattern.Name = "grpBoxPattern";
            this.grpBoxPattern.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpBoxPattern.Size = new System.Drawing.Size(485, 64);
            this.grpBoxPattern.TabIndex = 2;
            this.grpBoxPattern.TabStop = false;
            this.grpBoxPattern.Text = "Pattern";
            // 
            // comboBoxPatterns
            // 
            this.comboBoxPatterns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPatterns.FormattingEnabled = true;
            this.comboBoxPatterns.Location = new System.Drawing.Point(12, 25);
            this.comboBoxPatterns.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxPatterns.Name = "comboBoxPatterns";
            this.comboBoxPatterns.Size = new System.Drawing.Size(467, 24);
            this.comboBoxPatterns.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(311, 265);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(187, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "&Find Duplicates";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(12, 265);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(93, 265);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "L&oad";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // openQueryFileDialog
            // 
            this.openQueryFileDialog.FileName = "*.xml";
            this.openQueryFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            // 
            // saveQueryFileDialog
            // 
            this.saveQueryFileDialog.FileName = "*.xml";
            this.saveQueryFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 300);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.grpBoxPattern);
            this.Controls.Add(this.grpPaths);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Duplicate File Remover Tool";
            this.grpPaths.ResumeLayout(false);
            this.grpBoxPattern.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstPaths;
        private System.Windows.Forms.GroupBox grpPaths;
        private System.Windows.Forms.Button btnRemovePath;
        private System.Windows.Forms.Button btnAddPath;
        private System.Windows.Forms.GroupBox grpBoxPattern;
        private System.Windows.Forms.ComboBox comboBoxPatterns;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openQueryFileDialog;
        private System.Windows.Forms.SaveFileDialog saveQueryFileDialog;
    }
}

