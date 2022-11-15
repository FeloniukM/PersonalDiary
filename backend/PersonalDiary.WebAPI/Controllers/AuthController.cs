using Microsoft.AspNetCore.Mvc;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.User;

namespace PersonalDiary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthUserDTO>> Login(UserLoginDTO dto)
        {
            return Ok(await _authService.Authorize(dto));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            var createdUser = await _userService.CreateUser(user);
            var token = await _authService.GenerateAccessToken(createdUser.Id, createdUser.Nickname, createdUser.Email);

            var result = new AuthUserDTO
            {
                User = createdUser,
                Token = token
            };

            return Ok(result);
        }
    }
}
