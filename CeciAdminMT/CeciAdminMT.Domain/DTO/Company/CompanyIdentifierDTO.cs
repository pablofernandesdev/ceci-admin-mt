using Microsoft.AspNetCore.Mvc;

namespace CeciAdminMT.Domain.DTO.Company
{
    public class CompanyIdentifierDTO
    {
        /// <summary>
        /// Identifier company
        /// </summary>
        [BindProperty(Name = "companyId")]
        public int CompanyId { get; set; }
    }
}
