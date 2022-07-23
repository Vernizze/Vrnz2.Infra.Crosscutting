using Vrnz2.Infra.CrossCutting.Libraries.SecureObject;
using Vrnz2.Infra.CrossCutting.VOs.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static string HashCode(this object obj)
            => Convert.ToBase64String(obj.ToByteArray());

        public static string ToJson<T>(this T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}-{ex.StackTrace}");

                return null;
            }
        }

        public static Dictionary<string, Diff> Diff<T>(T baseObject, T toCompareObject)
            => Diff(baseObject, toCompareObject, baseObject.GetType().Name);

        private static Dictionary<string, Diff> Diff<T>(T baseObject, T toCompareObject, string prefix)
        {
            var result = new Dictionary<string, Diff>();
            int i = 0;

            var basePropertyInfos = baseObject.GetType().GetProperties().ToList();
            var toComparePropertyInfos = toCompareObject.IsNotNull() ? toCompareObject.GetType().GetProperties() : null;

            basePropertyInfos.SForEach(basePropertyInfo =>
            {
                var prop = basePropertyInfo.ReflectedType == typeof(string) ? baseObject : basePropertyInfo.GetValue(baseObject);
                var toCompareProp = toComparePropertyInfos.IsNotNull() && i < toComparePropertyInfos.Length ? basePropertyInfo.ReflectedType == typeof(string) ? toCompareObject : toComparePropertyInfos[i].GetValue(toCompareObject) : null;

                if (prop is IEnumerable && prop is not string && toCompareProp is IEnumerable && toCompareProp is not string)
                {
                    IList propList = (IList)basePropertyInfo.GetValue(baseObject, null);
                    IList toCompareList = (IList)basePropertyInfo.GetValue(toCompareObject, null);

                    int j = 0;

                    foreach (var item in propList)
                    {
                        if (item is IEnumerable && item is not string)
                        {
                            var listDiffs = Diff(item, toCompareList[j], string.Concat(prefix, ".", basePropertyInfo.Name));

                            listDiffs.SForEach(d => result.Add(d.Key, d.Value));
                        }
                        else
                        {
                            if (item.GetType().GetProperties().HaveAny())
                            {
                                if (item.GetType().GetProperties().HaveAny() && item.GetType().GetProperties().First().PropertyType.IsPrimitive)
                                {
                                    if (item.IsNotNull() && !item.Equals(toCompareProp))
                                    {
                                        var itemB = toCompareList.IsNotNull() && j < toCompareList.Count ? toCompareList[j] : null;

                                        result.Add(string.Concat(prefix, ".", basePropertyInfo.Name, $"[{j}]"), (item.ToString(), itemB?.ToString()));
                                    }
                                }
                                else
                                {
                                    if (toCompareList.IsNotNull() && toCompareList.Count > j)
                                    {
                                        var listDiffs = Diff(item, toCompareList[j], string.Concat(prefix, ".", basePropertyInfo.Name, $"[{j}]"));

                                        listDiffs.SForEach(d => result.Add(d.Key, d.Value));
                                    }
                                    else
                                    {
                                        result.Add(string.Concat(prefix, ".", basePropertyInfo.Name, $"[{j}]"), (item.ToString(), ""));
                                    }
                                }
                            }
                            else
                            {
                                result.Add(string.Concat(prefix, ".", basePropertyInfo.Name, $"[{j}]"), (item.ToString(), ""));
                            }
                        }

                        j++;
                    }
                }
                else
                {
                    if (prop.IsNotNull() && !prop.Equals(toCompareProp))
                        result.Add(string.Concat(prefix, ".", basePropertyInfo.Name), (prop.ToString(), toCompareProp?.ToString()));
                }

                i++;
            });

            return result;
        }

        public static SecureGetObject<T> ToNotNullSecureObject<T>(this T value)
            => new SecureGetObject<T>(value);

        public static SecureGetObject<T> ToSecureObject<T>(this T value)
            => value.IsNotNull() ? value.ToNotNullSecureObject() : SecureGetObject<T>.Empty;

        public static T GetValue<T>(this SecureGetObject<T> secureGetObject)
            => secureGetObject.IsEmpty ? default : secureGetObject.Value;
    }
}
