using Bogus;
using CeciAdminMT.Test.Fakers.User;

namespace CeciAdminMT.Test.Fakers.RegistrationToken
{
    public class RegistrationTokenFaker
    {
        public static Faker<CeciAdminMT.Domain.Entities.RegistrationToken> RegistrationTokenEntity()
        {
            return new Faker<CeciAdminMT.Domain.Entities.RegistrationToken>()
                .CustomInstantiator(p => new CeciAdminMT.Domain.Entities.RegistrationToken
                {
                    Active = true,
                    UserId = p.Random.Int(),
                    Id = p.Random.Int(),
                    RegistrationDate = p.Date.Recent(),
                    Token = p.Random.String2(30),
                    User = UserFaker.UserEntity().Generate()
                });
        }
    }
}
