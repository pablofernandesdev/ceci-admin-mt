using CeciAdminMT.Domain.DTO.Commons;
using CeciAdminMT.Domain.DTO.Import;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Service
{
    public interface IImportService
    {
        Task<ResultResponse> ImportUsersAsync(FileUploadDTO model);
    }
}
