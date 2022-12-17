using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.ViaCep;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Service.External
{
    public interface IViaCepService
    {
        Task<ResultResponse<ViaCepAddressResponseDTO>> GetAddressByZipCodeAsync(string zipCode);
    }
}
