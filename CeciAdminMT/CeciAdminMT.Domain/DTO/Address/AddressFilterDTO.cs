using CeciAdminMT.Domain.DTO.Commons;

namespace CeciAdminMT.Domain.DTO.Address
{
    public class AddressFilterDTO : QueryFilter
    {
        public string District { get; set; }

        public string Locality { get; set; }

        public string Uf { get; set; }
    }
}
