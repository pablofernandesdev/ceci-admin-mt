using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Repository
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Task<IEnumerable<Company>> GetByFilterAsync(CompanyFilterDTO filter);
        Task<int> GetTotalByFilterAsync(CompanyFilterDTO filter);
    }
}
