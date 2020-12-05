using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace Vrnz2.Infra.CrossCutting.Libraries.HttpClient
{
    public class JsonContent
        : StringContent
    {
        #region Constructors

        public JsonContent(object value)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
        {
        }

        public JsonContent(object value, string mediaType)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8, mediaType)
        {
        }

        #endregion
    }
}
