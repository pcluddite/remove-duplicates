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
using Baxendale.RemoveDuplicates.Search;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baxendale.RemoveDuplicates.Forms
{
    internal partial class ChecksumForm : Form
    {
        public ChecksumForm(Md5Hash hash)
        {
            InitializeComponent();
            txtChecksum.Text = hash.Base16;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtChecksum.Select(txtChecksum.Text.Length, 0);
            btnCopy.Select();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            txtChecksum.Focus();
            txtChecksum.SelectAll();
            Clipboard.SetText(txtChecksum.Text);
        }
    }
}
