using Bogus;
using CeciAdminMT.Domain.DTO.Company;
using System.Collections.Generic;

namespace CeciAdminMT.Test.Fakers.Company
{
    public static class CompanyFaker
    {
        public static Faker<CeciAdminMT.Domain.Entities.Company> CompanyEntity()
        {
            return new Faker<CeciAdminMT.Domain.Entities.Company>()
                .CustomInstantiator(p => new CeciAdminMT.Domain.Entities.Company
                {
                    Id = p.Random.Int(1, 99),
                    Name = p.Person.FullName,
                    Active = p.Random.Bool(),
                    ApiKey = p.Random.String(),
                    DocumentNumber = p.Random.String(),
                    RegistrationDate = p.Date.Recent(),
                    User = new List<CeciAdminMT.Domain.Entities.User>()
                });
        }

        public static Faker<CompanyAddDTO> CompanyAddDTO()
        {
            return new Faker<CompanyAddDTO>()
                .CustomInstantiator(p => new CompanyAddDTO
                {
                    Name = p.Person.FullName,
                    DocumentNumber = p.Random.String()
                });
        }

        public static Faker<CompanyUpdateDTO> CompanyUpdateDTO()
        {
            return new Faker<CompanyUpdateDTO>()
                .CustomInstantiator(p => new CompanyUpdateDTO
                {
                    Name = p.Person.FullName,
                    DocumentNumber = p.Random.String(),
                    CompanyId = p.Random.Int()
                });
        }

        public static Faker<CompanyDeleteDTO> CompanyDeleteDTO()
        {
            return new Faker<CompanyDeleteDTO>()
                .CustomInstantiator(p => new CompanyDeleteDTO
                {
                    CompanyId = p.Random.Int()
                });
        }

        public static Faker<CompanyFilterDTO> CompanyFilterDTO()
        {
            return new Faker<CompanyFilterDTO>()
                .CustomInstantiator(p => new CompanyFilterDTO
                {
                    Name = p.Person.FullName,
                    DocumentNumber = p.Random.String()
                });
        }

        public static Faker<CompanyResultDTO> CompanyResultDTO()
        {
            return new Faker<CompanyResultDTO>()
                .CustomInstantiator(p => new CompanyResultDTO
                {
                    Name = p.Person.FullName,
                    DocumentNumber = p.Random.String(),
                    CompanyId = p.Random.Int()
                });
        }
    }
}
