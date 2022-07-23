using System;
using System.Collections.Generic;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ParseExact<TEnum>(this TEnum source, string value, TEnum defaultValue = default)
            where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            if (Enum.IsDefined(typeof(TEnum), value.IsNumeric() ? int.Parse(value) : value))
            {
                source = (TEnum)Enum.Parse(typeof(TEnum), value);

                return source;
            }

            return defaultValue;
        }

        public static TEnum? ParseExact<TEnum>(this TEnum? source, string value, TEnum? defaultValue = default)
            where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            if (value.IsNotNull() && Enum.IsDefined(typeof(TEnum), value.IsNumeric() ? int.Parse(value) : value))
            {
                source = (TEnum)Enum.Parse(typeof(TEnum), value);

                return source;
            }

            return defaultValue;
        }

        public static TEnum? ParseExact<TEnum, TValue>(this TEnum? source, TValue value, TEnum? defaultValue = default)
            where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            if (value.IsNotNull() && ((value.IsNumeric() && Enum.IsDefined(typeof(TEnum), int.Parse(value.ToString()))) || (typeof(TValue).IsEnum && Enum.IsDefined(typeof(TEnum), (value as Enum).GetHashCode()))))
            {
                if (typeof(TValue).IsEnum)
                    source = (TEnum)Enum.Parse(typeof(TEnum), (value as Enum).GetHashCode().ToString());
                else
                    source = (TEnum)Enum.Parse(typeof(TEnum), value.ToString());

                return source;
            }

            return defaultValue;
        }

        public static List<TEnum> ToList<TEnum>(this TEnum source)
            where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var result = new List<TEnum>();

            foreach (TEnum element in Enum.GetValues(typeof(TEnum)))
                result.Add(element);

            return result;
        }
    }
}
