using System.IO;
using System.Net.Http;

namespace Vrnz2.Infra.CrossCutting.Libraries.HttpClient
{
    public class FileContent
        : MultipartFormDataContent
    {
        #region Constructors

        public FileContent(string filePath, string apiParamName)
        {
            var filestream = File.Open(filePath, FileMode.Open);
            var filename = Path.GetFileName(filePath);

            Add(new StreamContent(filestream), apiParamName, filename);
        }

        #endregion
    }
}
