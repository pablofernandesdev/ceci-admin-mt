using Bogus;
using CeciAdminMT.Domain.DTO.ValidationCode;
using CeciAdminMT.Infra.CrossCutting.Extensions;
using CeciAdminMT.Test.Fakers.User;

namespace CeciAdminMT.Test.Fakers.ValidationCode
{
    public class ValidationCodeFaker
    {
        public static Faker<CeciAdminMT.Domain.Entities.ValidationCode> ValidationCodeEntity()
        {
            return new Faker<CeciAdminMT.Domain.Entities.ValidationCode>()
                .CustomInstantiator(p => new CeciAdminMT.Domain.Entities.ValidationCode
                {
                    UserId = p.Random.Int(),
                    Active = true,
                    Code = PasswordExtension.EncryptPassword(p.Random.Word()),
                    Expires = p.Date.Future(),
                    Id = p.Random.Int(),
                    RegistrationDate = p.Date.Recent(),
                    User = UserFaker.UserEntity().Generate()
                });
        }

        public static Faker<ValidationCodeValidateDTO> ValidationCodeValidateDTO()
        {
            return new Faker<ValidationCodeValidateDTO>()
                .CustomInstantiator(p => new ValidationCodeValidateDTO
                {
                    Code = p.Random.Word()
                });
        }
    }
}
