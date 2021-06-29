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

namespace Baxendale.RemoveDuplicates
{
    internal class SizeFormatter
    {
        public const long UNIT_FACTOR = 1024;

        public const long BYTE_SIZE = 1;
        public const long KB_SIZE = BYTE_SIZE * UNIT_FACTOR;
        public const long MB_SIZE = KB_SIZE * UNIT_FACTOR;
        public const long GB_SIZE = MB_SIZE * UNIT_FACTOR;
        public const long TB_SIZE = GB_SIZE * UNIT_FACTOR;

        private static readonly long[] Limits = { BYTE_SIZE, KB_SIZE, MB_SIZE, GB_SIZE, TB_SIZE };
        private static readonly string[] Units = { "B", "KB", "MB", "GB", "TB" };
        
        public static string Format<TNumeric>(TNumeric n, int decimals)
            where TNumeric : struct, IConvertible, IComparable<TNumeric>, IEquatable<TNumeric>
        {
            double d = Convert.ToDouble(n);
            string format = decimals > 0 ? "#,#0.".PadRight(decimals + 5, '#') : "#,#0";
            int i = 0;
            while(i < Limits.Length && (long)d / Limits[i] >= UNIT_FACTOR)
                ++i;
            return $"{(d / Limits[i]).ToString(format)} {Units[i]}";
        }
    }
}
