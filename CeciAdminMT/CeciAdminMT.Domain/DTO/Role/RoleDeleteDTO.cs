using Microsoft.AspNetCore.Mvc;

namespace CeciAdminMT.Domain.DTO.Role
{
    public class RoleDeleteDTO
    {
        /// <summary>
        /// Identifier user
        /// </summary>
        [BindProperty(Name = "roleId")]
        public int RoleId { get; set; }
    }
}
