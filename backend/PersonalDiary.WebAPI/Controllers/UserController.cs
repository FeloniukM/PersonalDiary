using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalDiary.BLL.Interfaces;
using PersonalDiary.Common.DTO.User;
using PersonalDiary.WebAPI.Extensions;

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
        public async Task<IActionResult> InviteUser([FromBody] UserEmailDTO userInviteDTO)
        {
            var adminId = this.GetUserIdFromToken();
            await _userService.InviteUser(userInviteDTO, adminId);

            return Ok(userInviteDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInfo(Guid id)
        {
            var user = await _userService.GetUserInfo(id);

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeUserRole(UserEmailDTO userEmailDTO)
        {
            var adminId = this.GetUserIdFromToken();
            await _userService.ChangeUserRole(userEmailDTO, adminId);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = this.GetUserIdFromToken();
            await _userService.DeleteUser(userId);

            return NoContent();
        }
    }
}
