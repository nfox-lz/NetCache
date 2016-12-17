using System.Configuration;
using System.IO;

namespace Compete.NetCache
{
    internal static class Global
    {
        public static string StartupPath { get; private set; }

        public static string DataPath { get; private set; }

        public static string SettingsPath { get; private set; }

        static Global()
        {
            StartupPath = Path.GetDirectoryName(typeof(Global).Assembly.Location);

            DataPath = ConfigurationManager.AppSettings["DataPath"] ?? Path.Combine(StartupPath, "Data");
            if (!Directory.Exists(DataPath))
                Directory.CreateDirectory(DataPath);

            SettingsPath = Path.Combine(StartupPath, "settings.json");
        }
    }
}
