using System.Collections.Generic;

namespace Vrnz2.Infra.CrossCutting.Libraries.Csv
{
    public interface ICsvBuilder<T> 
        where T : class
    {
        string BuildCsvFromListOf(IEnumerable<T> data, bool includeHeader, char separator);
    }
}
