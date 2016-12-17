namespace Compete.NetCache.Models
{
    public sealed class Collection : ItemDictionary<byte[]>
    {
        public CollectionType Type { get; set; }
    }
}
