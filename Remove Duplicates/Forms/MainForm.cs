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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baxendale.RemoveDuplicates.Search;
using System.Reflection;

namespace Baxendale.RemoveDuplicates.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            Type patternType = typeof(FilePattern);
            foreach (FieldInfo field in patternType.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (field.Name.EndsWith("Files") && field.FieldType == typeof(FilePattern))
                {
                    comboBoxPatterns.Items.Add(field.GetValue(null));
                }
            }
            comboBoxPatterns.SelectedItem = FilePattern.AllFiles;
            base.OnLoad(e);
        }

        private void btnAddPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                lstPaths.Items.Add(folderBrowserDialog.SelectedPath);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (lstPaths.Items.Count == 0)
            {
                Program.ShowError(this, "You must specify at least one directory");
            }
            else
            {
                string[] paths = lstPaths.Items.Cast<string>().ToArray();
                string pattern = ((FilePattern)comboBoxPatterns.SelectedItem).Pattern;
                SearchForm searchForm = new SearchForm(paths, pattern);
                searchForm.ShowDialog(this);
            }
        }
    }
}
