using CeciAdminMT.Domain.DTO.Role;
using FluentValidation;

namespace CeciAdminMT.Service.Validators.Role
{
    public class RoleIdentifierValidator : AbstractValidator<IdentifierRoleDTO>
    {
        public RoleIdentifierValidator()
        {
            RuleFor(c => c.RoleId)
                .NotEmpty().WithMessage("Please enter the identifier role.")
                .NotNull().WithMessage("Please enter the identifier role.");
        }
    }
}
