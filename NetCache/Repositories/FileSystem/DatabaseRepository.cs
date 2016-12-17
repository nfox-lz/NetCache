using System;
using System.Collections.Generic;
using System.IO;

namespace Compete.NetCache.Repositories.FileSystem
{
    internal sealed class DatabaseRepository : IDatabaseRepository
    {
        public IDictionary<string, Models.Database> LoadDatabase(IEnumerable<Models.DatabaseSetting> settings)
        {
            var result = new Dictionary<string, Models.Database>();

            foreach (var setting in settings)
                try
                {
                    result.Add(setting.Name, LoadDatabase(setting));
                }
                catch (Exception exception)
                {
                    Logging.LogHelper.Logger.LogException(exception);
                }

            return result;
        }

        public Models.Database LoadDatabase(Models.DatabaseSetting setting)
        {
            if (!File.Exists(setting.SavePath))
                throw new Common.NetCacheException(string.Format("数据库文件不存在。【{0}】", setting.SavePath));

            return Common.BinHelper.Deserialize<Models.Database>(File.ReadAllBytes(setting.SavePath), setting.IsSaveCompression);
        }

        public void SaveDatabase(IDictionary<string, Models.Database> data)
        {
            foreach (var database in data)
                SaveDatabase(database.Value, database.Key);
        }

        public void SaveDatabase(Models.Database database, string name)
        {
            if (string.IsNullOrWhiteSpace(database.Options.SavePath))
            {
                database.Options.Name = name;
                var saveBasePath = Path.Combine(Global.DataPath, name);
                var savePath = Path.ChangeExtension(saveBasePath, ".db");
                ulong count = 0L;
                while (File.Exists(savePath))
                {
                    savePath = saveBasePath + count.ToString() + ".db";
                    count++;
                    if (count == ulong.MaxValue)
                        throw new Common.NetCacheException("数据库文件无法生成。");
                }

                database.Options.SavePath = savePath;
            }

            Common.BinHelper.Serialize(database, database.Options.SavePath, database.Options.IsSaveCompression);
        }
    }
}
