using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Compete.NetCache.Services
{
    internal sealed class SaveTimerService
    {
        private readonly Repositories.IDatabaseRepository databaseRepository;

        private readonly ICollection<Models.DatabaseSetting> databaseSettings;

        private readonly IDictionary<string, Models.Database> dataDictionary;

        private Timer saveTimer = new Timer();

        private static readonly IDictionary<string, DateTime> saveDateTimeDictionary = new Dictionary<string, DateTime>();

        public SaveTimerService(IDictionary<string, Models.Database> data, ICollection<Models.DatabaseSetting> settings, Repositories.IDatabaseRepository repository)
        {
            databaseRepository = repository;
            dataDictionary = data;
            databaseSettings = settings;

            saveTimer.Elapsed += SaveTimer_Elapsed;
        }

        private void SaveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var items = from saveDateTime in saveDateTimeDictionary
                        where saveDateTime.Value <= DateTime.Now
                        select saveDateTime.Key;

            foreach (var item in items)
            {
                databaseRepository.SaveDatabase(dataDictionary[item], item);
                saveDateTimeDictionary[item] = DateTime.Now.AddMilliseconds(dataDictionary[item].Options.SaveCycle);
            }

            var interval = ((from saveDateTime in saveDateTimeDictionary
                             select saveDateTime.Value).Min() - DateTime.Now).Milliseconds;
            saveTimer.Interval = interval < 0 ? 0 : interval;

            saveTimer.Start();
        }

        public void StartupSaveTimer()
        {
            saveTimer.Stop();

            saveDateTimeDictionary.Clear();
            var minCycle = -1;
            foreach (var item in dataDictionary)
            {
                if (item.Value.Options.SaveCycle <= 0)
                    continue;

                saveDateTimeDictionary.Add(item.Key, DateTime.Now.AddMilliseconds(item.Value.Options.SaveCycle));
                if (minCycle > item.Value.Options.SaveCycle)
                    minCycle = item.Value.Options.SaveCycle;
            }

            if (minCycle > 0)
            {
                saveTimer.Interval = minCycle;
                saveTimer.Start();
            }
        }

        public void StartupSaveTimer(string databaseName)
        {
            if (dataDictionary[databaseName].Options.SaveCycle <= 0)
                return;

            saveDateTimeDictionary[databaseName] = DateTime.Now.AddMilliseconds(dataDictionary[databaseName].Options.SaveCycle);

            var interval = ((from saveDateTime in saveDateTimeDictionary
                             select saveDateTime.Value).Min() - DateTime.Now).Milliseconds;
            saveTimer.Interval = interval < 0 ? 0 : interval;

            saveTimer.Start();
        }
    }
}
