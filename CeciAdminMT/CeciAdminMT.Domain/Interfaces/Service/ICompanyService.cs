using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.Company;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Service
{
    public interface ICompanyService
    {
        Task<ResultDataResponse<IEnumerable<CompanyResultDTO>>> GetAsync(CompanyFilterDTO filter);
        Task<ResultResponse> AddAsync(CompanyAddDTO obj);
        Task<ResultResponse> DeleteAsync(CompanyDeleteDTO obj);
        Task<ResultResponse> UpdateAsync(CompanyUpdateDTO obj);
        Task<ResultResponse<CompanyResultDTO>> GetByIdAsync(int id);
    }
}
