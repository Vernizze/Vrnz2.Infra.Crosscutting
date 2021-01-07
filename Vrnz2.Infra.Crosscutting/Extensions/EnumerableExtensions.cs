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

        public static T SFirstOrDefault<T>(this IEnumerable<T> value)
        {
            if (value.HaveAny())
                return value.ToList().FirstOrDefault();

            return (T)Activator.CreateInstance(typeof(T));
        }

        public static T SFirstOrDefault<T>(this IEnumerable<T> value, Func<T, bool> predicate)
        {
            if (value.HaveAny())
                return value.ToList().FirstOrDefault(predicate);

            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
