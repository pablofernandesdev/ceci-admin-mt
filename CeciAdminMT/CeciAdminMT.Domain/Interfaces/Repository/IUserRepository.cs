using CeciAdminMT.Domain.DTO.User;
using CeciAdminMT.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetByFilterAsync(UserFilterDTO filter);
        Task<int> GetTotalByFilterAsync(UserFilterDTO filter);
    }
}
