using DapperApp.Dto;
using DapperApp.Entities;

namespace DapperApp.Contracts
{
    public interface IUserRepository
    {
        public Task<User> GetUser(UserRequestDto userRequest);
    }
}
