using System;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object value)
            => value == null;

        public static bool IsNotNull(this object value)
            => !IsNull(value);

        public static bool IsDate(this object value)
            => value.IsNotNull() && DateTime.TryParse(value.ToString(), out _);

        public static bool IsNotDate(this object value)
            => !IsDate(value);

        public static bool IsNumeric(this object value)
            => value.IsNotNull() && decimal.TryParse(value.ToString(), out _);

        public static bool IsNotNumeric(this object value)
            => !IsNumeric(value);

        public static string HashCode(this object obj)
            => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
