using System.IO;
using System.Reflection;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetEmbeddedFileFromAssembly(this Assembly assembly, string resourceName)
        {
            var result = string.Empty;

            var fileName = assembly
                .GetManifestResourceNames()
                .SFirstOrDefault(f => f.Contains(resourceName));

            if (!string.IsNullOrEmpty(fileName))
            {
                using (Stream stream = assembly.GetManifestResourceStream(fileName))
                using (StreamReader reader = new StreamReader(stream))
                    result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
