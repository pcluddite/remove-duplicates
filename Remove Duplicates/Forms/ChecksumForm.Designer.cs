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
namespace Baxendale.RemoveDuplicates.Forms
{
    partial class ChecksumForm
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
            if (disposing && (components != null)) {
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
            txtChecksum = new System.Windows.Forms.TextBox();
            btnCopy = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // txtChecksum
            // 
            txtChecksum.BackColor = System.Drawing.SystemColors.Window;
            txtChecksum.Location = new System.Drawing.Point(12, 12);
            txtChecksum.Name = "txtChecksum";
            txtChecksum.ReadOnly = true;
            txtChecksum.Size = new System.Drawing.Size(222, 23);
            txtChecksum.TabIndex = 0;
            // 
            // btnCopy
            // 
            btnCopy.Location = new System.Drawing.Point(240, 11);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new System.Drawing.Size(75, 23);
            btnCopy.TabIndex = 1;
            btnCopy.Text = "Copy";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // ChecksumForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(320, 52);
            Controls.Add(btnCopy);
            Controls.Add(txtChecksum);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChecksumForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "MD5 Checksum";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtChecksum;
        private System.Windows.Forms.Button btnCopy;
    }
}