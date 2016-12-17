using System.Collections.Generic;

namespace Compete.NetCache.Repositories
{
    internal interface IDatabaseRepository
    {
        IDictionary<string, Models.Database> LoadDatabase(IEnumerable<Models.DatabaseSetting> settings);

        Models.Database LoadDatabase(Models.DatabaseSetting setting);

        void SaveDatabase(IDictionary<string, Models.Database> data);

        void SaveDatabase(Models.Database database, string name);
    }
}
