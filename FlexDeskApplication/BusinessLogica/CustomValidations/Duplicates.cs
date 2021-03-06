﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.CustomValidations
{
    public static class Duplicates
    {
        public static bool HasDuplicates<T>(this IEnumerable<T> subjects)
        {
            return HasDuplicates(subjects, EqualityComparer<T>.Default);
        }
        public static bool HasDuplicates<T>(this IEnumerable<T> subjects, IEqualityComparer<T> comparer)
        {
            if (subjects == null)
                throw new ArgumentNullException("subjects");

            if (comparer == null)
                throw new ArgumentNullException("comparer");

            var set = new HashSet<T>(comparer);

            foreach (var s in subjects)
                if (!set.Add(s))
                    return true;

            return false;
        }

    }
}
