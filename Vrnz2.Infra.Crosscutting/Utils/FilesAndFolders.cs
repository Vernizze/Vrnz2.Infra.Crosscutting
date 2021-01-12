using System.Collections.Generic;
using System.IO;
using System.Text;
using Vrnz2.Infra.CrossCutting.Extensions;

namespace Vrnz2.Infra.CrossCutting.Utils
{
    public static class FilesAndFolders
    {
        public static string AppPath()
            => Directory.GetCurrentDirectory();

        public static string GetFileContent(string file_name)
        {
            var result = string.Empty;

            if (File.Exists(file_name))
            {
                var file = File.ReadAllText(file_name);

                if (file.Length > 0)
                    result = file;
            }

            return result;
        }

        public static string GetFileContent(string file_name, Dictionary<string, string> pairs)
        {
            var result = string.Empty;

            if (File.Exists(file_name))
            {
                var file = File.ReadAllText(file_name);

                if (file.Length > 0)
                {
                    if (pairs.IsNotNull())
                        foreach (var pair in pairs)
                            file = file.Replace(pair.Key, pair.Value);

                    result = file;
                }
            }

            return result;
        }

        public static bool WriteFile(string file_path, string file_name, string content)
        {
            if (!Directory.Exists(file_path))
                Directory.CreateDirectory(file_path);

            using (StreamWriter file = new StreamWriter($@"{file_path}\{file_name}", File.Exists($@"{file_path}\{file_name}"), Encoding.UTF8))
                file.Write(content);

            return File.Exists($@"{file_path}\{file_name}");
        }
    }
}
