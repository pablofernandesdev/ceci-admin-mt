using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.User;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Service
{
    public interface IReportService
    {
        Task<ResultResponse<byte[]>> GenerateUsersReport(UserFilterDTO filter);
    }
}
