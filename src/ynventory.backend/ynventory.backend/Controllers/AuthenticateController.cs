using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ynventory.Backend.Contracts.Requests;
using Ynventory.Backend.Contracts.Responses;
using Ynventory.Backend.Exceptions;
using Ynventory.Backend.Resources;
using Ynventory.Backend.Services.Authentication;

namespace Ynventory.Backend.Controllers
{
    /// <summary>
    /// Provides functions to authenticate a user against the API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        /// <summary>
        /// Authenticates a user in the API
        /// </summary>
        /// <param name="request"></param>
        /// <returns>200 if succesful, or an <see cref="ErrorResponse"/> containing error information otherwise</returns>
        /// <remarks>
        /// Sample request
        /// 
        ///     POST /authenticate
        ///     {
        ///         "userName": "foo@example.com"
        ///         "password": "hunter12"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">The user was succesfully authenticated</response>
        /// <response code="401">The given password was invalid</response>
        /// <response code="404">The given user was not found</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            try
            {
                await _authenticateService.Authenticate(request.Username, request.Password);
            }
            catch (YnventoryException ex)
            {
                return new ErrorResponse(ex).ToResult();
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return Ok();
        }
    }
}
