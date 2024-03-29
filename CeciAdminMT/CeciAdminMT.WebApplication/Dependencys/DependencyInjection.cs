﻿using CeciAdminMT.Domain.Interfaces.Repository;
using CeciAdminMT.Domain.Interfaces.Service;
using CeciAdminMT.Domain.Interfaces.Service.External;
using CeciAdminMT.Infra.Data.Repository;
using CeciAdminMT.Service.Services;
using CeciAdminMT.Service.Services.External;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace CeciAdminMT.WebApplication.Dependencys
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection repositorys)
        {
            //repositorys
            repositorys.AddTransient<IRoleRepository, RoleRepository>();
            repositorys.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            repositorys.AddTransient<IUserRepository, UserRepository>();
            repositorys.AddTransient<IRegistrationTokenRepository, RegistrationTokenRepository>();
            repositorys.AddTransient<IValidationCodeRepository, ValidationCodeRepository>();
            repositorys.AddTransient<IAddressRepository, AddressRepository>();
            repositorys.AddTransient<ICompanyRepository, CompanyRepository>();
            repositorys.AddTransient<IUnitOfWork, UnitOfWork>();

            return repositorys;
        }

        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            //services
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IImportService, ImportService>();
            services.AddTransient<IValidationCodeService, ValidationCodeService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ICompanyService, CompanyService>();

            //external
            services.AddTransient<ISendGridService, SendGridService>();
            //services.AddTransient<IFirebaseService, FirebaseService>();

            services.AddHttpClient<IFirebaseService, FirebaseService>(client =>
            {
                var firebaseOptionsServerId = configuration["ExternalProviders:Firebase:ServerApiKey"];
                var firebaseOptionsSenderId = configuration["ExternalProviders:Firebase:SenderId"];

                client.BaseAddress = new Uri("https://fcm.googleapis.com");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                    $"key={firebaseOptionsServerId}");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={firebaseOptionsSenderId}");
            });

            services.AddHttpClient<IViaCepService, ViaCepService>(client =>
            {
                client.BaseAddress = new Uri(configuration["ExternalProviders:ViaCep:ApiUrl"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services;
        }
    }
}
