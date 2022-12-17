using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Infra.Data.Context;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CeciAdminMT.Infra.Data.Repository
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IUserRepository User { get; }
        public IRoleRepository Role { get; }
        public IRefreshTokenRepository RefreshToken { get; }
        public IRegistrationTokenRepository RegistrationToken { get; }
        public IValidationCodeRepository ValidationCode { get; }
        public IAddressRepository Address { get; }
        public ICompanyRepository Company { get; }

        public UnitOfWork(AppDbContext appDbContext,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IRegistrationTokenRepository registrationToken,
            IValidationCodeRepository validationCode,
            IAddressRepository address,
            ICompanyRepository company)
        {
            _appDbContext = appDbContext;
            User = userRepository;
            Role = roleRepository;
            RefreshToken = refreshTokenRepository;
            RegistrationToken = registrationToken;
            ValidationCode = validationCode;
            Address = address;
            Company = company;
        }

        public async Task CommitAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appDbContext.Dispose();
            }
        }
    }
}
