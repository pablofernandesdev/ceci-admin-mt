using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.Email;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Service
{
    public interface IEmailService
    {
        Task<ResultResponse> SendEmailAsync(EmailRequestDTO emailRequest);
    }
}
