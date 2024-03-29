﻿using System;
using System.Threading.Tasks;

namespace CeciAdminMT.Domain.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IRefreshTokenRepository RefreshToken { get; }
        IRegistrationTokenRepository RegistrationToken { get; }
        IValidationCodeRepository ValidationCode { get; }
        IAddressRepository Address { get; }
        ICompanyRepository Company { get; }
        Task CommitAsync();
    }
}
