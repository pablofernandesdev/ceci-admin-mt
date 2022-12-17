using System.Collections.Generic;

namespace CeciAdminMT.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public string ApiKey { get; set; }

        public string DocumentNumber { get; set; }

        public ICollection<User> User { get; set; }
    }
}
