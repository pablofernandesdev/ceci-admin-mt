using CeciAdminMT.Domain.Entities;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetBasicProfile();
    }
}
