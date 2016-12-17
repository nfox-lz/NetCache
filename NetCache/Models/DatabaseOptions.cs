namespace Compete.NetCache.Models
{
    public sealed class DatabaseOptions : DatabaseSetting
    {
        public CompressionMode CompressionMode { get; set; }

        public int SaveCycle { get; set; }

        public DatabaseOptions()
        {
            CompressionMode = CompressionMode.Transmission;
            SaveCycle = -1;
        }
    }
}
