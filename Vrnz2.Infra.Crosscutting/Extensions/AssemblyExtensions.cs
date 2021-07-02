using System.IO;
using System.Reflection;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class AssemblyExtensions
    {
        private static string GetEmbeddedFileFromAssembly(this Assembly assembly, string resourceName)
        {
            string result;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
                result = reader.ReadToEnd();

            return result;
        }
    }
}
