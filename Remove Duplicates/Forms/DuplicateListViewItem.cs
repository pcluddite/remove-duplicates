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
using System.Linq;
using System.Windows.Forms;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates.Forms
{
    internal class DuplicateListViewItem : ListViewItem
    {
        public DuplicateListViewItem(UniqueFile file)
        {
            Tag = file;
            SubItems.AddRange(new string[] { "", "", "" });
            UpdateValues();
        }

        public DuplicateListViewItem(UniqueFile file, string latestPath)
            : this(file)
        {
            AddPath(latestPath);
        }

        public UniqueFile File
        {
            get
            {
                return (UniqueFile)Tag;
            }
            set
            {
                Tag = value;
            }
        }

        public int DuplicateCount
        {
            get
            {
                return int.Parse(SubItems[0].Text);
            }
            set
            {
                SubItems[0].Text = value.ToString();
            }
        }

        public string Hash
        {
            get
            {
                return SubItems[1].Text;
            }
            set
            {
                SubItems[1].Text = value;
            }
        }

        public IEnumerable<string> Paths
        {
            get
            {
                return SubItems[2].Text.Split(';');
            }
            set
            {
                SubItems[2].Text = value.Aggregate((a, b) => a + ";" + b);
            }
        }

        public void UpdateValues()
        {
            DuplicateCount = File.Paths.Count;
            Hash = File.Hash.Base16;
            Paths = File.Paths;
        }

        public void AddPath(string newFilePath)
        {
            if (DuplicateCount > 0)
            {
                SubItems[2].Text += ';' + newFilePath;
            }
            else
            {
                SubItems[2].Text = newFilePath;
            }
            ++DuplicateCount;
        }
    }
}
