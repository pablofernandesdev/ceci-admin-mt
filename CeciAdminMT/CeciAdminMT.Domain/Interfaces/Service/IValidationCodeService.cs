using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.ValidationCode;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Service
{
    public interface IValidationCodeService
    {
        Task<ResultResponse> SendAsync();
        Task<ResultResponse> ValidateCodeAsync(ValidationCodeValidateDTO obj);
    }
}
