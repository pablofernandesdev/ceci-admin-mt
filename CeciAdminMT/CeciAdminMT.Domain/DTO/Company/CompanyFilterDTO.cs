using CeciAdminMT.Domain.DTO.Commons;

namespace CeciAdminMT.Domain.DTO.Company
{
    public class CompanyFilterDTO : QueryFilter
    {
        public string Name { get; set; }

        public string DocumentNumber { get; set; }
    }
}
