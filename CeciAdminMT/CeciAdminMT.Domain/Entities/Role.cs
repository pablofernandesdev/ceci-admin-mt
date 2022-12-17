using System.Collections.Generic;

namespace CeciAdminMT.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<User> User { get; set; }
    }
}
