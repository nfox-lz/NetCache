using Newtonsoft.Json;
using System.IO;

namespace Compete.NetCache.Common
{
    internal static class JsonHelper
    {
        public static T Load<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}
