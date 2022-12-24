using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Resources;
using Ynventory.Backend.Services.Identity;

namespace Ynventory.Backend.Controllers
{
    /// <summary>
    /// Provides functionalities for managing and retrieving users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Creates a new user for the given values
        /// </summary>
        /// <param name="request">The values to create the user with</param>
        /// <returns>The newly created user</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     POST /users
        ///     {
        ///         "userName": "fooBar",
        ///         "password": "hunter12"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">The user was successfully created</response>
        /// <response code="400">The user for the given user name already exists</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
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

        /// <summary>
        /// Retruns a user for a given id
        /// </summary>
        /// <param name="id">The id of the user to retrieve</param>
        /// <returns>The user associated with the id</returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     GET /users/123
        /// 
        /// </remarks>
        /// <response code="200">The user was found and returned</response>
        /// <response code="404">The user for the given id does not exist</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
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

        /// <summary>
        /// Attempts to change the password for the user
        /// </summary>
        /// <param name="id">The id of the user to change the password for</param>
        /// <param name="request">The old and new password</param>
        /// <returns></returns>
        /// <remarks>
        /// Example request:
        /// 
        ///     PATCH /users/123
        ///     {
        ///         "oldPassword": "hunter12",
        ///         "newPassword": "hunter22"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">The password was changed succesfully</response>
        /// <response code="401">The password was incorrect</response>
        /// <response code="404">The user was not found</response>
        [HttpPatch("{id:int}/change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
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
