using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Service
{
    public interface IRoleService
    {
        Task<ResultDataResponse<IEnumerable<RoleResultDTO>>> GetAsync();
        Task<ResultResponse> AddAsync(RoleAddDTO obj);
        Task<ResultResponse> DeleteAsync(int id);
        Task<ResultResponse> UpdateAsync(RoleUpdateDTO obj);
        Task<ResultResponse<RoleResultDTO>> GetByIdAsync(int id);
    }
}
