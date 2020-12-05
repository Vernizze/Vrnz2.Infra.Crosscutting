using System.Collections.Generic;
using System.Linq;

namespace Vrnz2.Infra.Crosscutting.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool HaveAny<T>(this IEnumerable<T> value)
            => value.IsNotNull() && value.Any();

        public static bool NotContains<T>(this IEnumerable<T> value, T item)
            => value.IsNotNull() && !value.Contains(item);
    }
}
