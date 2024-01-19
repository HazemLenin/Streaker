using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streaker.DAL.Dtos.Auth;
using Streaker.DAL.Dtos.Users;

namespace Streaker.DAL.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterUserAsync(RegisterDto registerUserDto);
        Task<AuthDto> GetTokenAsync(LoginDto loginDto);
        Task<AuthDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        Task<AuthDto> UpdateUserAsync(UpdateUserDto updateUserDto);
    }
}
