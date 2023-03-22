using Azure;
using DapperApp.Contracts;
using DapperApp.Dto;
using DapperApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public UsersController(IUserRepository _userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            this._userRepository = _userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        [HttpPost()]
        public async Task<IActionResult> Authenticate([FromBody] UserRequestDto userRequest)
        {
            var user = await _userRepository.GetUser(userRequest);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            return Ok(new {user, token});
        }
    }
}
