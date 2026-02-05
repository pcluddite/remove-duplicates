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
namespace Baxendale.RemoveDuplicates
{
    partial class Extensions
    {
        public static string FormatAsSize(this byte b, int decimals = 0)
        {
            return Sizes.Format(b, decimals);
        }

        public static string FormatAsSize(this sbyte s, int decimals = 0)
        {
            return Sizes.Format(s, decimals);
        }

        public static string FormatAsSize(this short s, int decimals = 0)
        {
            return Sizes.Format(s, decimals);
        }

        public static string FormatAsSize(this ushort u, int decimals = 0)
        {
            return Sizes.Format(u, decimals);
        }

        public static string FormatAsSize(this int i, int decimals = 0)
        {
            return Sizes.Format(i, decimals);
        }

        public static string FormatAsSize(this uint u, int decimals = 0)
        {
            return Sizes.Format(u, decimals);
        }

        public static string FormatAsSize(this long l, int decimals = 0)
        {
            return Sizes.Format(l, decimals);
        }

        public static string FormatAsSize(this ulong u, int decimals = 0)
        {
            return Sizes.Format(u, decimals);
        }

        public static string FormatAsSize(this float f, int decimals = 0)
        {
            return Sizes.Format(f, decimals);
        }

        public static string FormatAsSize(this double d, int decimals = 0)
        {
            return Sizes.Format(d, decimals);
        }

        public static string FormatAsSize(this decimal d, int decimals = 0)
        {
            return Sizes.Format(d, decimals);
        }

    }
}