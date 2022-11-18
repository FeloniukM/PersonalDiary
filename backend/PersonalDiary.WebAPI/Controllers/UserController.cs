using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.User;

namespace PersonalDiary.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("invite")]
        public async Task<IActionResult> InviteUser([FromBody] UserInviteDTO userInviteDTO)
        {
            await _userService.InviteUser(userInviteDTO);

            return Ok(userInviteDTO);
        }
    }
}
