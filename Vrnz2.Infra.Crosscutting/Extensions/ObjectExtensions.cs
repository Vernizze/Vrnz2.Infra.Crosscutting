using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        public static string ToJson<T>(this T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return null;
            }
        }

        public static T Clone<T>(this T obj)
            where T : class
        {
            object result = null;

            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();

                bf.Serialize(ms, obj);

                ms.Position = 0;

                result = bf.Deserialize(ms);

                ms.Close();
            }

            return (T)result;
        }

        public static string HashCode(this object obj)
            => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
