using System.Collections.Generic;

namespace Compete.NetCache.Models
{
    public abstract class ItemDictionary<T>
    {
        public IDictionary<string, T> Items { get; private set; }

        public T this[string key]
        {
            get
            {
                T item;
                return Items.TryGetValue(key, out item) ? item : default(T);

            }
            set
            {
                if (Items.ContainsKey(key))
                    Items[key] = value;
                else
                    Items.Add(key, value);
            }
        }

        public ItemDictionary()
        {
            Items = new Dictionary<string, T>();
        }

        public bool ContainsKey(string key)
        {
            return Items.ContainsKey(key);
        }
    }
}
