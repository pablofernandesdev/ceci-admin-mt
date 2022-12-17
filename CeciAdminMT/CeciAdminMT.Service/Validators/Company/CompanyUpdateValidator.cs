using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Domain.Interfaces.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeciAdminMT.Service.Validators.Company
{
    public class CompanyUpdateValidator : AbstractValidator<CompanyUpdateDTO>
    {
        private readonly IUnitOfWork _uow;

        public CompanyUpdateValidator(IUnitOfWork uow)
        {
            _uow = uow;

            RuleFor(c => c.CompanyId)
                .NotNull().WithMessage("Please enter the company.")
                .MustAsync(async (company, cancellation) => {
                    return await CompanyValid(company);
                }).WithMessage("Company invalid.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.DocumentNumber)
                 .NotEmpty().WithMessage("Please enter the document number.")
                 .NotNull().WithMessage("Please enter the document number.");
        }

        private async Task<bool> CompanyValid(int companyId)
        {
            return await _uow.Company.GetFirstOrDefaultNoTrackingAsync(x => x.Id.Equals(companyId)) != null;
        }
    }
}
