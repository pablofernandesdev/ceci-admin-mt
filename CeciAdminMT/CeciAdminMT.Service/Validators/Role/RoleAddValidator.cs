using CeciAdminMT.Domain.DTO.Role;
using FluentValidation;

namespace CeciAdminMT.Service.Validators.Role
{
    public class RoleAddValidator : AbstractValidator<RoleAddDTO>
    {
        public RoleAddValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name role.")
                .NotNull().WithMessage("Please enter the name role.");
        }
    }
}