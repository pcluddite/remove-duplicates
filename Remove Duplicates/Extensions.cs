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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Baxendale.RemoveDuplicates
{
    internal static class Extensions
    {
        public static bool EqualsArray(this Array array, Array other)
        {
            if (array == other) return true;
            if (array == null || other == null) return false;
            if (array.GetType() != other.GetType()) return false;

            Type comparerType = typeof(EqualityComparer<>).MakeGenericType(array.GetType().GetElementType());
            PropertyInfo defaultProperty = comparerType.GetProperty(nameof(EqualityComparer<object>.Default), BindingFlags.Public | BindingFlags.Static);

            return EqualsArray(array, other, (IEqualityComparer)defaultProperty.GetValue(null));
        }

        public static bool EqualsArray(this Array array, Array other, IEqualityComparer comparer)
        {
            if (array == other) return true;
            if (array == null || other == null) return false;
            if (array.GetType() != other.GetType()) return false;

            int rank = array.Rank;
            int[] indices = new int[rank];

            for (int leftBound = array.GetUpperBound(0); indices[0] > leftBound; ++indices[rank])
            {
                if (!comparer.Equals(array.GetValue(indices), other.GetValue(indices)))
                    return false;

                for (int dim = rank - 1; dim > 0; --dim)
                {
                    if (indices[dim] > array.GetUpperBound(dim))
                    {
                        for (int x = dim; x < rank; ++x)
                            indices[x] = 0;
                        ++indices[dim - 1];
                    }
                }
            }

            return true;
        }

        public static bool EqualsArray<T>(this T[] array, T[] other)
        {
            return array.EqualsArray(other, EqualityComparer<T>.Default);
        }

        public static bool EqualsArray<T>(this T[] array, T[] other, IEqualityComparer<T> comparer)
        {
            if (array == other) return true;
            if (array == null || other == null) return false;
            if (array.Length != other.Length) return false;

            for(int idx = 0, len = array.Length; idx < len; ++idx)
            {
                if (comparer.Equals(array[idx], other[idx]))
                    return false;
            }
            return true;
        }

        public static IEnumerable<T> Singleton<T>(this T o)
        {
            yield return o;
        }

        public static ISet<T> ToSet<T>(this IEnumerable<T> o)
        {
            return new HashSet<T>(o);
        }
    }
}
