using Vrnz2.Infra.CrossCutting.Libraries.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class FileExtensions
    {
        public static byte[] ReadToEnd(this Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static string ToCSV<T>(this List<T> list, bool include_header) where T : class
            => ToCSV<T>(list, include_header, ',');

        public static string ToCSV<T>(this List<T> list) where T : class
            => ToCSV<T>(list, true, ',');

        public static string ToCSV<T>(this List<T> list, bool include_header, char separator) where T : class
            => new CsvBuilder<T>().BuildCsvFromListOf(list, include_header, separator);

        public static byte[] ToByteCSV<T>(this List<T> list) where T : class
            => ToByteCSV<T>(list, ',');

        public static byte[] ToByteCSV<T>(this List<T> list, char separator) where T : class
            => Encoding.ASCII.GetBytes(new CsvBuilder<T>().BuildCsvFromListOf(list, true, separator));

        public static List<T> FromCSV<T>(this T destiny, string csv_content, char separator)
        {
            var result = new List<T>();

            string[] stringSeparators = new string[] { "\r\n" };
            string[] lines = csv_content.Split(stringSeparators, StringSplitOptions.None);

            lines.SForEach(l =>
            {
                string[] fields = l.Split(separator);

                destiny = CsvClassLoader.LoadNewCsv<T>(fields, true);

                result.Add(destiny);
            });

            return result;
        }
    }
}
