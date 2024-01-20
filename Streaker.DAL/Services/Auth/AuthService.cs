using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Streaker.Core.Domains;
using Streaker.DAL.Dtos.Auth;
using Streaker.DAL.Dtos.Users;

namespace Streaker.DAL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthDto> GetTokenAsync(LoginDto loginDto)
        {
            // Find user by Email/Username
            Regex emailRgx = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            ApplicationUser user;

            if (emailRgx.IsMatch(loginDto.Email))
                user = await _userManager.FindByEmailAsync(loginDto.Email);
            else
                user = await _userManager.FindByNameAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                Dictionary<string, List<string>> errors = new()
                {
                    { String.Empty, new() { "Incorrect email/password." } }
                };

                return new()
                {
                    Errors = errors
                };
            }

            return await GenerateUserTokens(user);
        }

        public async Task<AuthDto> RegisterUserAsync(RegisterDto registerUserDto)
        {
            var createUserResult = await _userManager.CreateAsync(new()
            {
                Email = registerUserDto.Email,
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                UserName = registerUserDto.UserName,
            }, registerUserDto.Password);

            if (!createUserResult.Succeeded)
                return new()
                {
                    Errors = GetErrorsFromIdentityResult(createUserResult)
                };

            var user = await _userManager.FindByNameAsync(registerUserDto.UserName);

            return await GenerateUserTokens(user);

        }

        public async Task<AuthDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            // Get user with that active refresh token
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenDto.RefreshToken && u.RefreshTokenExpiration < DateTime.UtcNow);

            if (user == null)
            {
                Dictionary<string, List<string>> errors = new()
                {
                    { String.Empty, new() { "The refresh token isn't active" } }
                };

                return new()
                {
                    Errors = errors,
                };
            }

            var tokens = await GenerateUserTokens(user);

            return tokens;
        }

        public async Task<AuthDto> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(updateUserDto.Id);

            if (user == null)
                return new()
                {
                    Errors = new()
                    {
                        { String.Empty, new() { "User not found with the given Id." }}
                    }
                };

            user.Email = updateUserDto.Email;
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.UserName = updateUserDto.UserName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new();
            }
            else
            {
                return new()
                {
                    Errors = GetErrorsFromIdentityResult(result)
                };
            }
        }

        private Dictionary<string, List<string>> GetErrorsFromIdentityResult(IdentityResult result)
        {
            Dictionary<string, List<string>> errors = new();

            foreach (var error in result.Errors)
            {
                errors.Add(error.Code, new() { error.Description });
            }

            return errors;
        }

        private async Task<AuthDto> GenerateUserTokens(ApplicationUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(30);

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new(ClaimTypes.Role, role));

            SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(_configuration["JsonWebTokenKeys:IssuerSigningKey"]));
            SigningCredentials signingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenOptions = new(
               claims: claims,
               expires: expiration,
               signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new();

            var token = tokenHandler.WriteToken(tokenOptions);

            var randomNumber = new byte[32];

            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);

            var refreshToken = Convert.ToBase64String(randomNumber);

            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = refreshTokenExpiration;

            await _userManager.UpdateAsync(user);

            return new()
            {
                Token = token,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = refreshTokenExpiration
            };
        }
    }
}
