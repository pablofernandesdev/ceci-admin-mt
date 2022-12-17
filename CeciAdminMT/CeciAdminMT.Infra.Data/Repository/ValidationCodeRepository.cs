using CeciAdminMT.Domain.Entities;
using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Infra.Data.Context;
using System.Diagnostics.CodeAnalysis;

namespace CeciAdminMT.Infra.Data.Repository
{
    [ExcludeFromCodeCoverage]
    public class ValidationCodeRepository : BaseRepository<ValidationCode>, IValidationCodeRepository
    {
        public ValidationCodeRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }
    }
}
