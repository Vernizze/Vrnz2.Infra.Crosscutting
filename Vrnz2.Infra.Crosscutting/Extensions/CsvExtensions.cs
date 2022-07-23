using System.Collections.Generic;
using System.Text;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class CsvExtensions
    {
        public static string ToHeaderString(this Queue<string> stack)
        {
            var headerString = new StringBuilder();

            foreach (var item in stack)
                headerString.Append(item);

            return headerString.ToString();
        }
    }
}
