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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxPatterns = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.grpPaths.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstPaths
            // 
            this.lstPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPaths.FormattingEnabled = true;
            this.lstPaths.Location = new System.Drawing.Point(4, 17);
            this.lstPaths.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstPaths.Name = "lstPaths";
            this.lstPaths.Size = new System.Drawing.Size(356, 82);
            this.lstPaths.TabIndex = 0;
            // 
            // grpPaths
            // 
            this.grpPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPaths.Controls.Add(this.btnRemovePath);
            this.grpPaths.Controls.Add(this.btnAddPath);
            this.grpPaths.Controls.Add(this.lstPaths);
            this.grpPaths.Location = new System.Drawing.Point(9, 10);
            this.grpPaths.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpPaths.Name = "grpPaths";
            this.grpPaths.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpPaths.Size = new System.Drawing.Size(364, 128);
            this.grpPaths.TabIndex = 1;
            this.grpPaths.TabStop = false;
            this.grpPaths.Text = "Search Paths";
            // 
            // btnRemovePath
            // 
            this.btnRemovePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemovePath.Location = new System.Drawing.Point(242, 105);
            this.btnRemovePath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRemovePath.Name = "btnRemovePath";
            this.btnRemovePath.Size = new System.Drawing.Size(56, 19);
            this.btnRemovePath.TabIndex = 2;
            this.btnRemovePath.Text = "Re&move";
            this.btnRemovePath.UseVisualStyleBackColor = true;
            // 
            // btnAddPath
            // 
            this.btnAddPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPath.Location = new System.Drawing.Point(303, 105);
            this.btnAddPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddPath.Name = "btnAddPath";
            this.btnAddPath.Size = new System.Drawing.Size(56, 19);
            this.btnAddPath.TabIndex = 1;
            this.btnAddPath.Text = "&Add";
            this.btnAddPath.UseVisualStyleBackColor = true;
            this.btnAddPath.Click += new System.EventHandler(this.btnAddPath_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBoxPatterns);
            this.groupBox1.Location = new System.Drawing.Point(9, 158);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(364, 52);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pattern";
            // 
            // comboBoxPatterns
            // 
            this.comboBoxPatterns.FormattingEnabled = true;
            this.comboBoxPatterns.Location = new System.Drawing.Point(9, 20);
            this.comboBoxPatterns.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxPatterns.Name = "comboBoxPatterns";
            this.comboBoxPatterns.Size = new System.Drawing.Size(351, 21);
            this.comboBoxPatterns.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(233, 215);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(140, 19);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "&Find Duplicates";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(9, 215);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 19);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(70, 215);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(56, 19);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "L&oad";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 244);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpPaths);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Duplicate File Remover Tool";
            this.grpPaths.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstPaths;
        private System.Windows.Forms.GroupBox grpPaths;
        private System.Windows.Forms.Button btnRemovePath;
        private System.Windows.Forms.Button btnAddPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxPatterns;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

