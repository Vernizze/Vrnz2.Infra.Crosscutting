using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Vrnz2.Infra.CrossCutting.Libraries.HttpClient
{
    public class PatchContent
        : StringContent
    {
        #region Constructors

        public PatchContent(object value)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json-patch+json")
        {
        }

        #endregion
    }
}
