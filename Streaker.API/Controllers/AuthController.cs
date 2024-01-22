using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streaker.API.Responses;
using Streaker.DAL.Dtos.Auth;
using Streaker.DAL.Dtos.Users;
using Streaker.DAL.Services.Auth;
using Streaker.DAL.Services.Users;

namespace Streaker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUsersService _usersService;

        public AuthController(IAuthService authService, IUsersService usersService)
        {
            _authService = authService;
            _usersService = usersService;
        }

        // GET: api/Auth/Profile
        [HttpGet("Profile")]
        [Authorize]
        public ActionResult<ApiResponse<UserDto>> Profile() =>
            Ok(new ApiResponse<UserDto>(_usersService.GetLoggedInUser(User)));

        // POST: api/Auth/Signup
        [HttpPost("Signup")]
        public async Task<ActionResult<Task<ApiResponse<AuthDto>>>> Signup(RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterUserAsync(registerDto);

            if (!registerResult.IsSucceed)
                return BadRequest(new ApiResponse()
                {
                    Errors = registerResult.Errors
                });
            return new ObjectResult(new ApiResponse<AuthDto>(registerResult))
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<AuthDto>>> Login(LoginDto loginDto)
        {
            var loginResult = await _authService.GetTokenAsync(loginDto);

            if (!loginResult.IsSucceed)
                return BadRequest(new ApiResponse()
                {
                    Errors = loginResult.Errors
                });

            return Ok(new ApiResponse<AuthDto>(loginResult));
        }

        // POST: api/Auth/Refresh
        [HttpPost("Refresh")]
        public async Task<ActionResult<ApiResponse<AuthDto>>> Refresh(RefreshTokenDto refreshTokenDto)
        {
            var refreshResult = await _authService.RefreshTokenAsync(refreshTokenDto);

            if (!refreshResult.IsSucceed)
                return BadRequest(new ApiResponse()
                {
                    Errors = refreshResult.Errors
                });

            return Ok(new ApiResponse<AuthDto>(refreshResult));
        }

        // GET: api/Auth/signin-google
        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(HandleGoogleCallback))
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        // GET: api/Auth/handle-google-callback
        [Authorize(GoogleDefaults.AuthenticationScheme)]
        [HttpGet("handle-google-callback")]
        public IActionResult HandleGoogleCallback()
        {
            // Handle the callback and retrieve user information
            // (You can access user information through User.Identity)

            return Ok();
        }

        // GET: api/Auth/signout-google
        [Authorize]
        [HttpGet("signout-google")]
        public IActionResult SignOutGoogle()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
