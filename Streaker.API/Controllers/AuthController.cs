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

        // GET: api/auth/profile
        [HttpGet("profile")]
        [Authorize]
        public ApiResponse<UserDto> Profile() => new()
        {
            Data = _usersService.GetLoggedInUser(User)
        };

        // POST: api/auth/signup
        [HttpPost("signup")]
        public async Task<ApiResponse<AuthDto>> Signup(RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterUserAsync(registerDto);

            if (!registerResult.IsSucceed)
                return new()
                {
                    Errors = registerResult.Errors
                };
            return new()
            {
                Data = registerResult
            };
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ApiResponse<AuthDto>> Login(LoginDto loginDto)
        {
            var loginResult = await _authService.GetTokenAsync(loginDto);

            if (!loginResult.IsSucceed)
                return new()
                {
                    Errors = loginResult.Errors
                };

            return new()
            {
                Data = loginResult
            };
        }

        // POST: api/auth/refresh
        [HttpPost("refresh")]
        public async Task<ApiResponse<AuthDto>> Refresh(RefreshTokenDto refreshTokenDto)
        {
            var refreshResult = await _authService.RefreshTokenAsync(refreshTokenDto);

            if (!refreshResult.IsSucceed)
                return new()
                {
                    Errors = refreshResult.Errors
                };

            return new()
            {
                Data = refreshResult
            };
        }

        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(HandleGoogleCallback)),
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpGet("handle-google-callback")]
        public IActionResult HandleGoogleCallback()
        {
            // Handle the callback and retrieve user information
            // (You can access user information through User.Identity)

            return Ok();
        }

        [Authorize]
        [HttpGet("signout-google")]
        public IActionResult SignOutGoogle()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
