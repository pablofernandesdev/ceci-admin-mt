using Microsoft.AspNetCore.Mvc;

namespace CeciAdminMT.Domain.DTO.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentifierRoleDTO
    {
        /// <summary>
        /// Identifier user
        /// </summary>
        [BindProperty(Name = "roleId")]
        public int RoleId { get; set; }
    }
}
