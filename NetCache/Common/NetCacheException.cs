using System;

namespace Compete.NetCache.Common
{
    internal sealed class NetCacheException : Exception
    {
        public NetCacheException()
        { }

        public NetCacheException(string message)
            : base(message)
        { }
    }
}
