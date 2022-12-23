using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Resources;
using Ynventory.Backend.Services.Identity;

namespace Ynventory.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserCreateRequest request)
        {
            try
            {
                var user = await _userService.CreateUser(request);
                return CreatedAtAction("GetUser", new
                {
                    user.Id
                }, user);
            }
            catch (UserAlreadyExistsException ex)
            {
                return ErrorResponse.BadRequest(ErrorCodes.User.UserAlreadyExists, ex.Message).AsResult();
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                return Ok(await _userService.GetUser(id));
            }
            catch (UserNotFoundException ex)
            {
                return ErrorResponse.NotFound(ErrorCodes.User.UserNotFound, ex.Message).AsResult();
            }
        }

        [HttpPatch("{id:int}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, UserChangePasswordRequest request)
        {
            try
            {
                await _userService.ChangePassword(id, request);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return ErrorResponse.NotFound(ErrorCodes.User.UserNotFound, ex.Message).AsResult();
            }
            catch (InvalidPasswordException ex)
            {
                return ErrorResponse.Unauthorized(ErrorCodes.Authentication.InvalidPassword, ex.Message).AsResult();
            }
        }

    }
}
