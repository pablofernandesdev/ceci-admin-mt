using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Domain.Interfaces.Repository;
using FluentValidation;
using System.Threading.Tasks;

namespace CeciAdminMT.Service.Validators.Company
{
    public class CompanyDeleteValidator : AbstractValidator<CompanyDeleteDTO>
    {
        private readonly IUnitOfWork _uow;

        public CompanyDeleteValidator(IUnitOfWork uow)
        {
            _uow = uow;

            RuleFor(c => c.CompanyId)
                .NotEmpty().WithMessage("Please enter the identifier company.")
                .NotNull().WithMessage("Please enter the identifier company.")
                .MustAsync(async (companyId, cancellation) => {
                    return await CompanyValid(companyId);
                }).WithMessage("Company invalid.");
        }

        private async Task<bool> CompanyValid(int companyId)
        {
            return await _uow.Company.GetFirstOrDefaultNoTrackingAsync(x => x.Id.Equals(companyId)) != null;
        }
    }
}
