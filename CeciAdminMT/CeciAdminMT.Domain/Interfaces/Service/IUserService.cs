using System.Collections.Generic;
using System.Threading.Tasks;
using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.User;

namespace CeciAdminMT.Domain.Interfaces.Service
{
    public interface IUserService 
    {
        Task<ResultDataResponse<IEnumerable<UserResultDTO>>> GetAsync(UserFilterDTO filter);
        Task<ResultResponse> AddAsync(UserAddDTO obj);
        Task<ResultResponse> DeleteAsync(UserDeleteDTO obj);
        Task<ResultResponse> UpdateAsync(UserUpdateDTO obj);
        Task<ResultResponse<UserResultDTO>> GetByIdAsync(int id);
        Task<ResultResponse> UpdateRoleAsync(UserUpdateRoleDTO obj);
    }
}
