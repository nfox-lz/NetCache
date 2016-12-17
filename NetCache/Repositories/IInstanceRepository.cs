using System.Collections.Generic;

namespace Compete.NetCache.Repositories
{
    internal interface IInstanceRepository
    {
        ICollection<Models.DatabaseSetting> LoadSettings();

        void SaveSettings(IEnumerable<Models.DatabaseSetting> databaseSettings);
    }
}
