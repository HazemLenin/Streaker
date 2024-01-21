﻿using Microsoft.AspNetCore.Authentication.Cookies;
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
        public ApiResponse<UserDto> Profile() => new()
        {
            Data = _usersService.GetLoggedInUser(User)
        };

        // POST: api/Auth/Signup
        [HttpPost("Signup")]
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

        // POST: api/Auth/Login
        [HttpPost("Login")]
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

        // POST: api/Auth/Refresh
        [HttpPost("Refresh")]
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

        // GET: api/Auth/signin-google
        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(HandleGoogleCallback))
            };

            Console.WriteLine(Url.Action(nameof(HandleGoogleCallback)));

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        // GET: api/Auth/handle-google-callback
        [Authorize]
        [HttpGet("handle-google-callback")]
        public IActionResult HandleGoogleCallback()
        {
            Console.WriteLine("callback");
            Console.WriteLine(User.Identity.Name);
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
