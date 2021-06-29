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
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Baxendale.DataManagement.Xml;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates.Forms
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            foreach (FieldInfo field in typeof(FilePattern).GetFields(BindingFlags.Static | BindingFlags.Public))
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
            ToggleSaveButton();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (lstPaths.Items.Count == 0)
            {
                Program.ShowError(this, "You must specify at least one directory");
                return;
            }

            try
            {
                FilePattern pattern = GetSelectedPattern();

                string[] paths = lstPaths.Items.Cast<string>().ToArray();
                SearchForm searchForm = new SearchForm(paths, pattern);
                searchForm.ShowDialog(this);
            }
            catch (Exception ex) when (ex is ArgumentException)
            {
                Program.ShowError(this, ex.Message);
            }
        }

        private FilePattern GetSelectedPattern()
        {
            if (comboBoxPatterns.SelectedItem == null)
            {
                if (string.IsNullOrEmpty(comboBoxPatterns.Text))
                {
                    throw new ArgumentException("You mast specify a pattern");
                }
                else
                {
                    return new FilePattern(comboBoxPatterns.Text);
                }
            }
            return ((FilePattern)comboBoxPatterns.SelectedItem);
        }

        private void lstPaths_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemovePath.Enabled = lstPaths.SelectedIndex > -1;
            ToggleSaveButton();
        }

        private void btnRemovePath_Click(object sender, EventArgs e)
        {
            for (int index = lstPaths.SelectedIndices.Count - 1; index >= 0; --index)
            {
                lstPaths.Items.RemoveAt(lstPaths.SelectedIndices[index]);
            }
            ToggleSaveButton();
        }

        private void ToggleSaveButton()
        {
            btnSave.Enabled = lstPaths.Items.Count > 0 && comboBoxPatterns.Text.Length > 0;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openQueryFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    QueryFile file = XmlSerializer.Deserialize<QueryFile>(XDocument.Load(openQueryFileDialog.FileName).Root);
                    lstPaths.Items.Clear();
                    foreach (string path in file.SearchPaths)
                    {
                        lstPaths.Items.Add(path);
                    }

                    comboBoxPatterns.Text = file.Pattern.ToString();
                }
                catch (Exception ex) when (ex is IOException || ex is XmlException || ex is ArgumentException)
                {
                    Program.ShowError(this, ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveQueryFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    QueryFile file = new QueryFile(lstPaths.Items.Cast<string>(), GetSelectedPattern());
                    XDocument doc = new XDocument(XmlSerializer.Serialize(file, "query"));
                    doc.Save(saveQueryFileDialog.FileName);
                }
                catch (Exception ex) when (ex is IOException || ex is XmlException || ex is ArgumentException)
                {
                    Program.ShowError(this, ex.Message);
                }
            }
        }

    }
}
