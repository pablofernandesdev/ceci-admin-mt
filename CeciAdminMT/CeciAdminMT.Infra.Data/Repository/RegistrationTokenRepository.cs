using CeciAdminMT.Domain.Entities;
using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Infra.Data.Context;
using System.Diagnostics.CodeAnalysis;

namespace CeciAdminMT.Infra.Data.Repository
{
    [ExcludeFromCodeCoverage]
    public class RegistrationTokenRepository : BaseRepository<RegistrationToken>, IRegistrationTokenRepository
    {
        public RegistrationTokenRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }
    }
}
