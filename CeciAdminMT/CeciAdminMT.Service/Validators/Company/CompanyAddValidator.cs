using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Domain.Interfaces.Repository;
using FluentValidation;
using System.Threading.Tasks;

namespace CeciAdminMT.Service.Validators.Company
{
    public class CompanyAddValidator : AbstractValidator<CompanyAddDTO>
    {
        private readonly IUnitOfWork _uow;

        public CompanyAddValidator(IUnitOfWork uow)
        {
            _uow = uow;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.DocumentNumber)
                .NotEmpty().WithMessage("Please enter the document number.")
                .NotNull().WithMessage("Please enter the document number.")
                .MustAsync(async (documentNumber, cancellation) => {
                    return !await RegisteredDocumentNumber(documentNumber);
                }).WithMessage("Document number already registered.");
        }

        private async Task<bool> RegisteredDocumentNumber(string documentNumber)
        {
            return await _uow.Company.GetFirstOrDefaultNoTrackingAsync(x => x.DocumentNumber.Equals(documentNumber)) != null;
        }
    }
}
