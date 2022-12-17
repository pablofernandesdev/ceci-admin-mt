using Microsoft.AspNetCore.Http;

namespace CeciAdminMT.Domain.DTO.Import
{
    public class FileUploadDTO
    {
        /// <summary>
        /// File for import
        /// </summary>
        public IFormFile File { get; set; }
    }
}
