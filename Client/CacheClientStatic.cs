using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Compete.NetCache
{
    public partial class CacheClient
    {
        private static bool isCompression = true;

        public static string GetStringValue(string databaseName, string collectionName, string key)
        {
            var data = GetBinValue(databaseName, collectionName, key);
            return data == null ? null : Encoding.GetEncoding("UTF-8").GetString(data);
        }

        public static void SetStringValue(string databaseName, string collectionName, string key, string data)
        {
            SetBinValue(databaseName, collectionName, key, Encoding.GetEncoding("UTF-8").GetBytes(data));
        }

        public static async Task<string> GetStringValueAsync(string databaseName, string collectionName, string key)
        {
            var data = await GetBinValueAsync(databaseName, collectionName, key);
            return data == null ? null : Encoding.GetEncoding("UTF-8").GetString(data);
        }

        public static void SetStringValueAsync(string databaseName, string collectionName, string key, string data)
        {
            SetBinValueAsync(databaseName, collectionName, key, Encoding.GetEncoding("UTF-8").GetBytes(data));
        }

        private static byte[] Compress(byte[] data)
        {
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

                    var result = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(result, 0, result.Length);

                    return result;
                }
                finally
                {
                    stream.Close();
                }
        }

        private static byte[] Decompress(byte[] data)
        {
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

                                return result;
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
        }

        private static async Task<byte[]> CompressAsync(byte[] data)
        {
            using (var stream = new MemoryStream())
                try
                {
                    using (GZipStream zipStream = new GZipStream(stream, CompressionLevel.Fastest, true))
                        try
                        {
                            await zipStream.WriteAsync(data, 0, data.Length);
                        }
                        finally
                        {
                            zipStream.Close();
                        }

                    var result = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(result, 0, result.Length);
                    return result;
                }
                finally
                {
                    stream.Close();
                }
        }

        private static async Task<byte[]> DecompressAsync(byte[] data)
        {
            using (var stream = new MemoryStream(data))
                try
                {
                    using (GZipStream zipStream = new GZipStream(stream, CompressionMode.Decompress))
                        try
                        {
                            using (MemoryStream outBuffer = new MemoryStream())
                            {
                                byte[] buffer = new byte[65536];
                                int count = await zipStream.ReadAsync(buffer, 0, buffer.Length);
                                while (count > 0)
                                {
                                    outBuffer.Write(buffer, 0, count);
                                    count = await zipStream.ReadAsync(buffer, 0, buffer.Length);
                                }
                                var result = outBuffer.ToArray();
                                outBuffer.Close();

                                return result;
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
        }

        public static byte[] GetBinValue(string databaseName, string collectionName, string key)
        {
            var data = WcfServiceHelper.Execute(service => service.GetValue(databaseName, collectionName, key));

            return data == null ? null : isCompression ? Decompress(data) : data;
        }

        public static void SetBinValue(string databaseName, string collectionName, string key, byte[] data)
        {
            WcfServiceHelper.Execute(service => service.SetValue(databaseName, collectionName, key, isCompression ? Compress(data) : data));
        }

        public static async Task<byte[]> GetBinValueAsync(string databaseName, string collectionName, string key)
        {
            var data = await WcfServiceHelper.Execute(service => service.GetValueAsync(databaseName, collectionName, key));

            return data == null ? null : isCompression ? await DecompressAsync(data) : data;
        }

        public static async void SetBinValueAsync(string databaseName, string collectionName, string key, byte[] data)
        {
            await WcfServiceHelper.Execute(async service => service.SetValueAsync(databaseName, collectionName, key, isCompression ? await CompressAsync(data) : data));
        }

        public static T GetValue<T>(string databaseName, string collectionName, string key)
        {
            var json = GetStringValue(databaseName, collectionName, key);
            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }

        public static void SetValue<T>(string databaseName, string collectionName, string key, T data)
        {
            SetStringValue(databaseName, collectionName, key, JsonConvert.SerializeObject(data));
        }

        public static async Task<T> GetValueAsync<T>(string databaseName, string collectionName, string key)
        {
            var json = await GetStringValueAsync(databaseName, collectionName, key);
            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }

        public static void SetValueAsync<T>(string databaseName, string collectionName, string key, T data)
        {
            SetStringValueAsync(databaseName, collectionName, key, JsonConvert.SerializeObject(data));
        }

        public static void Save(string databaseName = null)
        {
            WcfServiceHelper.Execute(service => service.Save(databaseName));
        }

        public static async void SaveAsync(string databaseName = null)
        {
            await WcfServiceHelper.Execute(service => service.SaveAsync(databaseName));
        }

        public static bool Remove(string databaseName, string collectionName, string key)
        {
            return WcfServiceHelper.Execute(service => service.Remove(databaseName, collectionName, key));
        }

        public static async Task<bool> RemoveAsync(string databaseName, string collectionName, string key)
        {
            return await WcfServiceHelper.Execute(async service => await service.RemoveAsync(databaseName, collectionName, key));
        }

        public static bool CreateCollection(string databaseName, string collectionName)
        {
            return WcfServiceHelper.Execute(service => service.CreateCollection(databaseName, collectionName));
        }

        public static async Task<bool> CreateCollectionAsync(string databaseName, string collectionName)
        {
            return await WcfServiceHelper.Execute(async service => await service.CreateCollectionAsync(databaseName, collectionName));
        }

        public static bool DropCollection(string databaseName, string collectionName)
        {
            return WcfServiceHelper.Execute(service => service.DropCollection(databaseName, collectionName));
        }

        public static async Task<bool> DropCollectionAsync(string databaseName, string collectionName)
        {
            return await WcfServiceHelper.Execute(async service => await service.DropCollectionAsync(databaseName, collectionName));
        }

        public static bool CreateDatabase(string databaseName)
        {
            return WcfServiceHelper.Execute(service => service.CreateDatabase(databaseName));
        }

        public static async Task<bool> CreateDatabaseAsync(string databaseName)
        {
            return await WcfServiceHelper.Execute(async service => await service.CreateDatabaseAsync(databaseName));
        }

        public static bool DropDatabase(string databaseName)
        {
            return WcfServiceHelper.Execute(service => service.DropDatabase(databaseName));
        }

        public static async Task<bool> DropDatabaseAsync(string databaseName)
        {
            return await WcfServiceHelper.Execute(async service => await service.DropDatabaseAsync(databaseName));
        }
    }
}
