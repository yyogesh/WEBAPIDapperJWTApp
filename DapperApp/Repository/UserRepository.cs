using Dapper;
using DapperApp.Context;
using DapperApp.Contracts;
using DapperApp.Dto;
using DapperApp.Entities;
using System.Data;

namespace DapperApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser(UserRequestDto userRequest)
        {
            var query = "select userid, name, email, role from tbl_user where userid = @UserName and password = @Password";
            var parameters = new DynamicParameters();
            parameters.Add("UserName", userRequest.UserName, DbType.String);
            parameters.Add("Password", userRequest.Password, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, parameters);
                return user;
            }
        }
    }
}
