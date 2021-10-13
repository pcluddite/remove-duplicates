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
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Baxendale.Data.Xml;
using Baxendale.RemoveDuplicates.Forms;
using Baxendale.RemoveDuplicates.Search;

namespace Baxendale.RemoveDuplicates
{
    internal class AppContext : ApplicationContext
    {
        static AppContext()
        {
            XmlSerializer.Default.RegisterType<Query>("query");
            XmlSerializer.Default.RegisterType<SearchResult>("result");

            XmlSerializer.Default.RegisterType<FileInfo>("uri",
                (s, o, n) => new XAttribute(n, o.FullName),
                (s, x) => new FileInfo(x.Value));
        }

        public AppContext(string[] args)
        {
            MainForm main = new MainForm();
            main.FormClosed += Main_FormClosed;
            main.Show();
            if (args.Length > 0)
            {
                if (args.Length > 1 && args[0] == "--start")
                {
                    if (main.LoadQuery(args[1]))
                    {
                        main.StartSearch();
                    }
                }
                else
                {
                    main.LoadQuery(args[0]);
                }
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExitThread();
        }
    }
}
