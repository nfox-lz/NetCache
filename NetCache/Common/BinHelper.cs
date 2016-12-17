using Compete.NetCache.Extensions;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Compete.NetCache.Common
{
    public static class BinHelper
    {
        public static byte[] Serialize(object obj, bool isCompression = true)
        {
            byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(obj.ToJson());

            if (isCompression)
                using (var stream = new MemoryStream())
                    try
                    {
                        using (GZipStream zipStream = new GZipStream(stream, CompressionLevel.Fastest, true))
                            try
                            {
                                zipStream.Write(data, 0, data.Length);
                            }
                            finally
                            {
                                zipStream.Close();
                            }

                        var binData = new byte[stream.Length];
                        stream.Position = 0;
                        stream.Read(binData, 0, binData.Length);

                        return binData;
                    }
                    finally
                    {
                        stream.Close();
                    }
            else
                return data;
        }

        public static void Serialize(object obj, string path, bool isCompression = true)
        {
            File.WriteAllBytes(path, Serialize(obj, isCompression));
        }

        public static T Deserialize<T>(byte[] data, bool isCompression = true)
        {
            byte[] binData;
            if (isCompression)
                using (var stream = new MemoryStream(data))
                    try
                    {
                        using (GZipStream zipStream = new GZipStream(stream, CompressionMode.Decompress))
                            try
                            {
                                using (MemoryStream outBuffer = new MemoryStream())
                                {
                                    byte[] buffer = new byte[65536];
                                    int count = zipStream.Read(buffer, 0, buffer.Length);
                                    while (count > 0)
                                    {
                                        outBuffer.Write(buffer, 0, count);
                                        count = zipStream.Read(buffer, 0, buffer.Length);
                                    }
                                    var result = outBuffer.ToArray();
                                    outBuffer.Close();

                                    binData = result;
                                }
                            }
                            finally
                            {
                                zipStream.Close();
                            }
                    }
                    finally
                    {
                        stream.Close();
                    }
            else
                binData = data;

            return JsonConvert.DeserializeObject<T>(Encoding.GetEncoding("UTF-8").GetString(binData));
        }

        public static T Deserialize<T>(string path, bool isCompression = true)
        {
            return Deserialize<T>(File.ReadAllBytes(path), isCompression);
        }
    }
}
