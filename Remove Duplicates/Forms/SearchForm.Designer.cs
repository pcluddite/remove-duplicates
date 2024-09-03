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
            lstPaths = new System.Windows.Forms.ListBox();
            grpPaths = new System.Windows.Forms.GroupBox();
            checkSubdirs = new System.Windows.Forms.CheckBox();
            btnRemovePath = new System.Windows.Forms.Button();
            btnAddPath = new System.Windows.Forms.Button();
            grpBoxPattern = new System.Windows.Forms.GroupBox();
            comboBoxPatterns = new System.Windows.Forms.ComboBox();
            btnSearch = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            btnLoad = new System.Windows.Forms.Button();
            folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            openQueryFileDialog = new System.Windows.Forms.OpenFileDialog();
            saveQueryFileDialog = new System.Windows.Forms.SaveFileDialog();
            grpPaths.SuspendLayout();
            grpBoxPattern.SuspendLayout();
            SuspendLayout();
            // 
            // lstPaths
            // 
            lstPaths.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lstPaths.FormattingEnabled = true;
            lstPaths.ItemHeight = 15;
            lstPaths.Location = new System.Drawing.Point(4, 20);
            lstPaths.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            lstPaths.Name = "lstPaths";
            lstPaths.Size = new System.Drawing.Size(413, 109);
            lstPaths.TabIndex = 0;
            lstPaths.SelectedIndexChanged += lstPaths_SelectedIndexChanged;
            // 
            // grpPaths
            // 
            grpPaths.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            grpPaths.Controls.Add(checkSubdirs);
            grpPaths.Controls.Add(btnRemovePath);
            grpPaths.Controls.Add(btnAddPath);
            grpPaths.Controls.Add(lstPaths);
            grpPaths.Location = new System.Drawing.Point(10, 11);
            grpPaths.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            grpPaths.Name = "grpPaths";
            grpPaths.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            grpPaths.Size = new System.Drawing.Size(423, 166);
            grpPaths.TabIndex = 1;
            grpPaths.TabStop = false;
            grpPaths.Text = "Search Paths";
            // 
            // checkSubdirs
            // 
            checkSubdirs.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            checkSubdirs.AutoSize = true;
            checkSubdirs.Checked = true;
            checkSubdirs.CheckState = System.Windows.Forms.CheckState.Checked;
            checkSubdirs.Location = new System.Drawing.Point(10, 137);
            checkSubdirs.Name = "checkSubdirs";
            checkSubdirs.Size = new System.Drawing.Size(142, 19);
            checkSubdirs.TabIndex = 1;
            checkSubdirs.Text = "Include subdirectories";
            checkSubdirs.UseVisualStyleBackColor = true;
            // 
            // btnRemovePath
            // 
            btnRemovePath.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnRemovePath.Enabled = false;
            btnRemovePath.Location = new System.Drawing.Point(283, 134);
            btnRemovePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnRemovePath.Name = "btnRemovePath";
            btnRemovePath.Size = new System.Drawing.Size(66, 22);
            btnRemovePath.TabIndex = 3;
            btnRemovePath.Text = "Re&move";
            btnRemovePath.UseVisualStyleBackColor = true;
            btnRemovePath.Click += btnRemovePath_Click;
            // 
            // btnAddPath
            // 
            btnAddPath.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnAddPath.Location = new System.Drawing.Point(353, 134);
            btnAddPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnAddPath.Name = "btnAddPath";
            btnAddPath.Size = new System.Drawing.Size(66, 22);
            btnAddPath.TabIndex = 2;
            btnAddPath.Text = "&Add";
            btnAddPath.UseVisualStyleBackColor = true;
            btnAddPath.Click += btnAddPath_Click;
            // 
            // grpBoxPattern
            // 
            grpBoxPattern.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            grpBoxPattern.Controls.Add(comboBoxPatterns);
            grpBoxPattern.Location = new System.Drawing.Point(10, 182);
            grpBoxPattern.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            grpBoxPattern.Name = "grpBoxPattern";
            grpBoxPattern.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            grpBoxPattern.Size = new System.Drawing.Size(423, 60);
            grpBoxPattern.TabIndex = 2;
            grpBoxPattern.TabStop = false;
            grpBoxPattern.Text = "Pattern";
            // 
            // comboBoxPatterns
            // 
            comboBoxPatterns.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxPatterns.FormattingEnabled = true;
            comboBoxPatterns.Location = new System.Drawing.Point(10, 23);
            comboBoxPatterns.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            comboBoxPatterns.Name = "comboBoxPatterns";
            comboBoxPatterns.Size = new System.Drawing.Size(408, 23);
            comboBoxPatterns.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnSearch.Location = new System.Drawing.Point(271, 248);
            btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(164, 22);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "&Find Duplicates";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnSave.Location = new System.Drawing.Point(10, 248);
            btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(66, 22);
            btnSave.TabIndex = 4;
            btnSave.Text = "&Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnLoad.Location = new System.Drawing.Point(81, 248);
            btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new System.Drawing.Size(66, 22);
            btnLoad.TabIndex = 5;
            btnLoad.Text = "L&oad";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // openQueryFileDialog
            // 
            openQueryFileDialog.FileName = "*.xml";
            openQueryFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            // 
            // saveQueryFileDialog
            // 
            saveQueryFileDialog.FileName = "*.xml";
            saveQueryFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(444, 281);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Controls.Add(btnSearch);
            Controls.Add(grpBoxPattern);
            Controls.Add(grpPaths);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimumSize = new System.Drawing.Size(460, 320);
            Name = "MainForm";
            Text = "Duplicate File Remover Tool";
            grpPaths.ResumeLayout(false);
            grpPaths.PerformLayout();
            grpBoxPattern.ResumeLayout(false);
            ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox checkSubdirs;
    }
}

