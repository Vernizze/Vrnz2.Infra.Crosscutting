using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        private const int TAIL_SIZE = 8192;
        private static readonly List<string> _booleanValidValues = new List<string> { "0", "1" };

        public static string GetMd5(this string value)
        {
            var sBuilder = new StringBuilder();
            var md5Hash = MD5.Create();

            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }

        public static string RemoveSpecialCharacters(this string value)
        {
            var result = string.Empty;

            if (!string.IsNullOrEmpty(value))
                result = Regex.Replace(value, @"[^0-9a-zA-Z]+", string.Empty);

            return result;
        }

        public static bool IsBoolean(this string value)
            => !string.IsNullOrEmpty(value) && _booleanValidValues.Contains(value) || bool.TryParse(value, out _);

        public static bool IsNumeric(this string value)
            => decimal.TryParse(value, out _);

        public static bool IsNotNumeric(this string value)
            => !IsNumeric(value);

        public static string OnlyNumeric(this string value)
        {
            var result = string.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                foreach (var chr in value)
                    if (Char.IsNumber(chr))
                        result = string.Concat(result, chr);
            }
            else
                result = "0";

            return result;
        }
    }
}
