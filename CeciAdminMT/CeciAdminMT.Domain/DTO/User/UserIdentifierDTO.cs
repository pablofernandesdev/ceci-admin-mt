using Microsoft.AspNetCore.Mvc;

namespace CeciAdminMT.Domain.DTO.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserIdentifierDTO
    {
        /// <summary>
        /// Identifier user
        /// </summary>
        [BindProperty(Name = "userId")]
        public int UserId { get; set; }
    }
}
