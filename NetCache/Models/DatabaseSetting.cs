namespace Compete.NetCache.Models
{
    public class DatabaseSetting
    {
        public string Name { get; set; }

        public string SavePath { get; set; }

        public bool IsSaveCompression { get; set; }

        public DatabaseSetting()
        {
            IsSaveCompression = true;
        }
    }
}
