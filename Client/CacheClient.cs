using Newtonsoft.Json;
using System;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Compete.NetCache
{
    public partial class CacheClient : IDisposable
    {
        //private Server.INetCacheService service = new Server.NetCacheServiceClient();
        private Server.NetCacheServiceClient service = new Server.NetCacheServiceClient();

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    if (service.State == CommunicationState.Opened)
                        service.Close();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~CacheClient() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

        public CacheClient()
        {
            DatabaseName = "database";
        }

        public CacheClient(string databaseName)
        {
            DatabaseName = databaseName;
        }

        public void Open()
        {
            service.Open();
        }

        public void Close()
        {
            service.Close();
        }

        private string _databaseName;

        public string DatabaseName
        {
            get
            {
                return _databaseName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _databaseName = value;
            }
        }

        public string GetStringValue(string collectionName, string key)
        {
            var data = GetBinValue(collectionName, key);
            return data == null ? null : Encoding.GetEncoding("UTF-8").GetString(data);
        }

        public void SetStringValue(string collectionName, string key, string data)
        {
            SetBinValue(collectionName, key, Encoding.GetEncoding("UTF-8").GetBytes(data));
        }

        public async Task<string> GetStringValueAsync(string collectionName, string key)
        {
            var data = await GetBinValueAsync(collectionName, key);
            return data == null ? null : Encoding.GetEncoding("UTF-8").GetString(data);
        }

        public async Task SetStringValueAsync(string collectionName, string key, string data)
        {
            await SetBinValueAsync(collectionName, key, Encoding.GetEncoding("UTF-8").GetBytes(data));
        }

        public byte[] GetBinValue(string collectionName, string key)
        {
            var data = service.GetValue(DatabaseName, collectionName, key);

            return data == null ? null : isCompression ? Decompress(data) : data;
        }

        public void SetBinValue(string collectionName, string key, byte[] data)
        {
            service.SetValue(DatabaseName, collectionName, key, isCompression ? Compress(data) : data);
        }

        public async Task<byte[]> GetBinValueAsync(string collectionName, string key)
        {
            var data = await service.GetValueAsync(DatabaseName, collectionName, key);

            return data == null ? null : isCompression ? await DecompressAsync(data) : data;
        }

        public async Task SetBinValueAsync(string collectionName, string key, byte[] data)
        {
            await service.SetValueAsync(DatabaseName, collectionName, key, isCompression ? await CompressAsync(data) : data);
        }

        public T GetValue<T>(string collectionName, string key)
        {
            var json = GetStringValue(collectionName, key);
            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }

        public void SetValue<T>(string collectionName, string key, T data)
        {
            SetStringValue(collectionName, key, JsonConvert.SerializeObject(data));
        }

        public async Task<T> GetValueAsync<T>(string collectionName, string key)
        {
            var json = await GetStringValueAsync(collectionName, key);
            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }

        public async void SetValueAsync<T>(string collectionName, string key, T data)
        {
            await SetStringValueAsync(collectionName, key, JsonConvert.SerializeObject(data));
        }

        public void Save()
        {
            service.Save(DatabaseName);
        }

        public async Task SaveAsync()
        {
            await service.SaveAsync(DatabaseName);
        }

        public bool Remove(string collectionName, string key)
        {
            return service.Remove(DatabaseName, collectionName, key);
        }

        public async Task<bool> RemoveAsync(string collectionName, string key)
        {
            return await service.RemoveAsync(DatabaseName, collectionName, key);
        }

        public bool CreateCollection(string collectionName)
        {
            return service.CreateCollection(DatabaseName, collectionName);
        }

        public async Task<bool> CreateCollectionAsync(string collectionName)
        {
            return await service.CreateCollectionAsync(DatabaseName, collectionName);
        }

        public bool DropCollection(string collectionName)
        {
            return service.DropCollection(DatabaseName, collectionName);
        }

        public async Task<bool> DropCollectionAsync(string collectionName)
        {
            return await service.DropCollectionAsync(DatabaseName, collectionName);
        }
    }
}
