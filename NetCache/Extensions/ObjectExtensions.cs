using Newtonsoft.Json;
using System.IO;

namespace Compete.NetCache.Extensions
{
    internal static class ObjectExtensions
    {
        public static string ToJson(this object obj, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        public static void SaveJson(this object obj, string path, Formatting formatting = Formatting.None)
        {
            File.WriteAllText(path, obj.ToJson(formatting));
        }
    }
}
