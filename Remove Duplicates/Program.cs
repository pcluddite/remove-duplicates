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
using System.Windows.Forms;
using Baxendale.RemoveDuplicates.Forms;

namespace Baxendale.RemoveDuplicates
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static DialogResult ShowError(IWin32Window owner, Exception ex)
        {
            return ShowError(owner, ex);
        }

        public static DialogResult ShowError(IWin32Window owner, string message)
        {
            return ShowError(owner, message, MessageBoxButtons.OK);
        }

        public static DialogResult ShowError(IWin32Window owner, string message, MessageBoxButtons buttons)
        {
            return ShowDialog(owner, message, buttons, MessageBoxIcon.Error);
        }

        public static DialogResult ShowInfo(IWin32Window owner, string message)
        {
            return ShowInfo(owner, message, MessageBoxButtons.OK);
        }

        public static DialogResult ShowInfo(IWin32Window owner, string message, MessageBoxButtons buttons)
        {
            return ShowDialog(owner, message, buttons, MessageBoxIcon.Information);
        }

        public static DialogResult ShowDialog(IWin32Window owner, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(owner, message, (owner as Form)?.Name ?? Application.ProductName, buttons, icon);
        }
    }
}
