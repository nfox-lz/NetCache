using System.Collections.Generic;

namespace Compete.NetCache.Models
{
    public sealed class Database : ItemDictionary<Collection>
    {
        public DatabaseOptions Options { get; set; }

        public ICollection<User> Users { get; private set; }

        public Database()
        {
            Options = new DatabaseOptions();
            Users = new List<User>();
        }
    }
}
