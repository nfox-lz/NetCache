using System.Collections.Generic;

namespace Compete.NetCache.Models
{
    public sealed class User
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public ICollection<CollectionOperatingAuthorization> CollectionAuthorization { get; private set; }

        public User()
        {
            CollectionAuthorization = new List<CollectionOperatingAuthorization>();
        }
    }
}
