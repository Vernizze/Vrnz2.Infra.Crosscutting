using System;
using System.Collections.Generic;
using System.Linq;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool HaveAny<T>(this IEnumerable<T> value)
            => value.IsNotNull() && value.Any();

        public static bool HaveAny<T>(this IEnumerable<T> value, Func<T, bool> predicate)
            => value.IsNotNull() && value.Any(predicate);

        public static bool NotContains<T>(this IEnumerable<T> value, T item)
            => value.IsNotNull() && !value.Contains(item);

        public static IEnumerable<T> SForEach<T>(this IEnumerable<T> value, Action<T> action)
        {
            if (value.HaveAny())
                value.ToList().ForEach(f => action(f));

            return value;
        }

        public static IEnumerable<T> SWhere<T>(this IEnumerable<T> value, Func<T, bool> predicate)
        {
            if (value.HaveAny())
                return value.Where(predicate);

            return new List<T>();
        }

        public static T SFirstOrDefault<T>(this IEnumerable<T> value)
        {
            if (value.HaveAny())
                return value.ToList().FirstOrDefault();

            return default;
        }

        public static T SFirstOrDefault<T>(this IEnumerable<T> value, Func<T, bool> predicate)
        {
            if (value.HaveAny())
                return value.ToList().FirstOrDefault(predicate);

            return default;
        }

        public static List<T> SToList<T>(this IEnumerable<T> value)
        {
            if (value.HaveAny())
                return value.ToList();

            return default;
        }

        public static IEnumerable<IGrouping<TKey, T>> SGroupBy<T, TKey>(this IEnumerable<T> value, Func<T, TKey> predicate)
        {
            if (value.HaveAny())
                return value.GroupBy(predicate);

            return default;
        }

        public static bool SRemove<T>(this List<T> value, T item)
        {
            if (value.HaveAny())
                return value.Remove(item);

            return false;
        }

        public static IEnumerable<T> SExcept<T>(this IEnumerable<T> value, IEnumerable<T> second)
        {
            if (value.HaveAny())
            {
                if (!second.HaveAny())
                    return value;

                value = value.Except(second);

                return value;
            }

            return default;
        }

        public static IEnumerable<T> SOrderBy<T, TKey>(this IEnumerable<T> value, Func<T, TKey> predicate)
        {
            if (value.HaveAny())
            {
                value = value.OrderBy(predicate);

                return value;
            }

            return default;
        }

        public static IEnumerable<T> SOrderByDescending<T, TKey>(this IEnumerable<T> value, Func<T, TKey> predicate)
        {
            if (value.HaveAny())
            {
                value = value.OrderByDescending(predicate);

                return value;
            }

            return default;
        }

        public static void SClear<T>(this List<T> value)
        {
            if (value.HaveAny())
                value.Clear();
        }

        public static int SCount<T>(this IEnumerable<T> value)
        {
            if (value.HaveAny())
                return value.Count();

            return 0;
        }

        public static IList<T> AddIfNotNull<T>(this IList<T> list, T value, Func<bool> check = null)
        {
            if (list.IsNotNull() && value.IsNotNull() && (check.IsNull() || (check.IsNotNull() && check())))
                list.Add(value);

            return list;
        }

        public static IList<T> AddRangeIfNotNull<T>(this IList<T> list, IList<T> values, Func<bool> check = null)
        {
            if (list.IsNotNull() && values.HaveAny() && (check.IsNull() || (check.IsNotNull() && check())))
                values.SForEach(value => list.AddIfNotNull(value));

            return list;
        }
    }
}
