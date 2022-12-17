using CeciAdminMT.Domain.DTO.Company;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeciAdminMT.Service.Validators.Company
{
    public class CompanyIdentifierValidator : AbstractValidator<CompanyIdentifierDTO>
    {
        public CompanyIdentifierValidator()
        {
            RuleFor(c => c.CompanyId)
                .NotEmpty().WithMessage("Please enter the identifier company.")
                .NotNull().WithMessage("Please enter the identifier company.");
        }
    }
}
