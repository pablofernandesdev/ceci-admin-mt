using Microsoft.AspNetCore.Mvc;

namespace CeciAdminMT.Domain.DTO.Company
{
    public class CompanyDeleteDTO
    {
        /// <summary>
        /// Identifier company
        /// </summary>
        [BindProperty(Name = "companyId")]
        public int CompanyId { get; set; }
    }
}
