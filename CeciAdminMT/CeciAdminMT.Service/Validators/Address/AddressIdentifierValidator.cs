using CeciAdminMT.Domain.DTO.Address;
using FluentValidation;

namespace CeciAdminMT.Service.Validators.Address
{
    public class AddressIdentifierValidator : AbstractValidator<AddressIdentifierDTO>
    {
        public AddressIdentifierValidator()
        {
            RuleFor(c => c.AddressId)
                .NotEmpty().WithMessage("Please enter the address id.")
                .NotNull().WithMessage("Please enter the address id.");
        }
    }
}
