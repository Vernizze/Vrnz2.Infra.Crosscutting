using System;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class EnumExtensions
    {
        public static Enum ParseExact<TEnum>(this Enum root, string value)
        {
            if (Enum.IsDefined(typeof(TEnum), value))
                root = (Enum)Enum.Parse(typeof(TEnum), value);

            return root;
        }
    }
}
