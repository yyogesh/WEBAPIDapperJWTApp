using DapperApp.Entities;

namespace DapperApp.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
