using CeciAdminMT.Domain.DTO.Address;
using CeciAdminMT.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Repository
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Task<IEnumerable<Address>> GetLoggedUserAddressesAsync(int userId, AddressFilterDTO filter);
        Task<int> GetTotalLoggedUserAddressesAsync(int userId, AddressFilterDTO filter);
        Task<IEnumerable<Address>> GetByFilterAsync(AddressFilterDTO filter);
        Task<int> GetTotalByFilterAsync(AddressFilterDTO filter);
    }
}
