using CeciAdminMT.Domain.DTO.User;

namespace CeciAdminMT.Domain.Interfaces.Service
{
    public interface ITokenService
    {
        public string GenerateToken(UserResultDTO model);
    }
}
