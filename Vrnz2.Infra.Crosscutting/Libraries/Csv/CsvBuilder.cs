using Vrnz2.Infra.CrossCutting.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Vrnz2.Infra.CrossCutting.Libraries.Csv
{
    public class CsvBuilder<T> 
        : ICsvBuilder<T> 
        where T : class
    {
        #region Variables

        private readonly Queue<string> headers = new Queue<string>();

        #endregion

        #region Methods

        public string BuildCsvFromListOf(IEnumerable<T> data, bool includeHeader, char separator)
        {
            var csv = new StringBuilder();

            if (includeHeader)
                BuilderHeader(csv, separator);

            AddRows(data, csv, separator);

            return RemoveFinalNewLine(csv.ToString());
        }

        internal void BuilderHeader(StringBuilder csv, char separator)
        {
            var headerNames = new StringBuilder();

            BuilderHeaderData(typeof(T).GetProperties(), headerNames, separator);
            RemoveTrailingComma(headerNames);

            csv.Append(headerNames);
            csv.AppendLine();
        }

        private void BuilderHeaderData(PropertyInfo[] properties, StringBuilder headerNames, char separator)
        {
            for (var i = 0; i < properties.Length; i++)
            {
                if (!(PropertyIsAClassThatIsNotAString(properties[i]) || PropertyIsAStruct(properties[i])))
                {
                    if (PropertyIsAnInterfaceOrChar(properties[i]))
                        continue;
                    headerNames.Append(string.Format("{0}{1}", headers.ToHeaderString(), properties[i].Name));
                    headerNames.Append(separator);
                }

                if (i == properties.Length - 1)
                    if (headers.Count > 0)
                        headers.Dequeue();
            }
        }

        private void AddRows(IEnumerable<T> data, StringBuilder csv, char separator)
        {
            var rowValues = new StringBuilder();

            foreach (var row in data)
            {
                BuildRowData(rowValues, row, separator);
                RemoveTrailingComma(rowValues);

                rowValues.AppendLine();
            }

            csv.Append(rowValues);
        }

        private void BuildRowData<TRowData>(StringBuilder rowValues, TRowData type, char separator)
        {
            var properties = type.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (!(PropertyIsAClassThatIsNotAString(property) || PropertyIsAStruct(property)))
                {
                    if (PropertyIsAnInterfaceOrChar(property))
                        continue;
                    var value = property.GetValue(type, null);
                    rowValues.Append(value);
                    rowValues.Append(separator);
                }
            }
        }

        private bool PropertyIsAnInterfaceOrChar(PropertyInfo property)
            => ((property.PropertyType.IsInterface) || (property.PropertyType == typeof(char)));

        private bool PropertyIsAnObject(PropertyInfo property)
            => (property.PropertyType == typeof(object));

        private bool PropertyIsAClassThatIsNotAString(PropertyInfo property)
            => property.PropertyType.IsClass && property.PropertyType != typeof(string);

        private bool PropertyIsAStruct(PropertyInfo property)
            => ((property.PropertyType.IsValueType && !property.PropertyType.IsClass && !property.PropertyType.IsPrimitive && !property.PropertyType.IsEnum) &&
                    (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(Decimal) && property.PropertyType != typeof(Guid)));

        private static void RemoveTrailingComma(StringBuilder stringBuilder)
            => stringBuilder.Remove(stringBuilder.Length - 1, 1);

        private string RemoveFinalNewLine(string csv)
            => csv.TrimEnd('\r', '\n');

        #endregion
    }
}
