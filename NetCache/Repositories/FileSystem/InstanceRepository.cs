using Compete.NetCache.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Compete.NetCache.Repositories.FileSystem
{
    internal sealed class InstanceRepository : IInstanceRepository
    {
        public ICollection<Models.DatabaseSetting> LoadSettings()
        {
            return File.Exists(Global.SettingsPath) ? Common.JsonHelper.Load<ICollection<Models.DatabaseSetting>>(Global.SettingsPath) : new List<Models.DatabaseSetting>();
        }

        public void SaveSettings(IEnumerable<Models.DatabaseSetting> databaseSettings)
        {
            databaseSettings.SaveJson(Global.SettingsPath, Formatting.Indented);
        }
    }
}
