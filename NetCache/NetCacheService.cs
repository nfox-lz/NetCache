using System;
using System.Collections.Generic;
using System.Linq;

namespace Compete.NetCache
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“NetCacheService”。
    public class NetCacheService : INetCacheService
    {
        private static readonly Repositories.IInstanceRepository instanceRepository = new Repositories.FileSystem.InstanceRepository();

        private static readonly Repositories.IDatabaseRepository databaseRepository = new Repositories.FileSystem.DatabaseRepository();

        private static readonly IDictionary<string, Models.Database> dataDictionary;

        private static readonly Services.SaveTimerService saveTimerService;

        static NetCacheService()
        {
            var databaseSettings = instanceRepository.LoadSettings();
            dataDictionary = databaseRepository.LoadDatabase(databaseSettings);
            saveTimerService = new Services.SaveTimerService(dataDictionary, databaseSettings, databaseRepository);

            saveTimerService.StartupSaveTimer();
        }

        private static T GetItem<T>(IDictionary<string, T> dictionary, string key)
        {
            if (!dictionary.ContainsKey(key))
                lock (dictionary)
                    if (!dictionary.ContainsKey(key))
                        dictionary.Add(key, Activator.CreateInstance<T>());
            return dictionary[key];
        }

        private static void SetItem<T>(IDictionary<string, T> dictionary, string key, T item)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = item;
            else
                lock (dictionary)
                    if (!dictionary.ContainsKey(key))
                        dictionary.Add(key, item);
        }

        private static Models.Collection GetCollection(string databaseName, string collectionName)
        {
            Models.Database database;
            return dataDictionary.TryGetValue(databaseName, out database) ? database[collectionName] : null;
        }

        #region 公开接口

        public byte[] GetValue(string databaseName, string collectionName, string key)
        {
            return GetCollection(databaseName, collectionName)?[key];
        }

        public void SetValue(string databaseName, string collectionName, string key, byte[] data)
        {
            SetItem(GetItem(GetItem(dataDictionary, databaseName).Items, collectionName).Items, key, data);
        }

        public bool Remove(string databaseName, string collectionName, string key)
        {
            var collection = GetCollection(databaseName, collectionName);
            return collection == null ? false : collection.Items.Remove(key);
        }

        public bool CreateCollection(string databaseName, string collectionName)
        {
            Models.Database database;
            if (dataDictionary.TryGetValue(databaseName, out database))
            {
                if (database.Items.ContainsKey(collectionName))
                    return false;

                database.Items.Add(collectionName, new Models.Collection());
                return true;
            }

            return false;
        }

        public bool DropCollection(string databaseName, string collectionName)
        {
            Models.Database database;
            return dataDictionary.TryGetValue(databaseName, out database) ? database.Items.Remove(collectionName) : false;
        }

        public bool CreateDatabase(string databaseName)
        {
            if (dataDictionary.ContainsKey(databaseName))
                return false;

            dataDictionary.Add(databaseName, new Models.Database());
            return true;
        }

        public bool DropDatabase(string databaseName)
        {
            return dataDictionary.Remove(databaseName);
        }

        public void Save(string databaseName = null)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                databaseRepository.SaveDatabase(dataDictionary);

                instanceRepository.SaveSettings(from setting in dataDictionary
                                                select new Models.DatabaseSetting()
                                                {
                                                    Name = setting.Value.Options.Name,
                                                    SavePath = setting.Value.Options.SavePath,
                                                    IsSaveCompression = setting.Value.Options.IsSaveCompression,
                                                });

                saveTimerService.StartupSaveTimer();
            }
            else if (dataDictionary.ContainsKey(databaseName))
            {
                databaseRepository.SaveDatabase(dataDictionary[databaseName], databaseName);
                saveTimerService.StartupSaveTimer(databaseName);
            }
            //instanceRepository.SaveSettings(databaseSettings);
        }

        #endregion // 公开接口
    }
}
