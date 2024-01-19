using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Streaker.DAL.Dtos.Users;
using System.Security.Claims;

namespace Streaker.DAL.Services.Users
{
    public interface IUsersService
    {
        UserDto GetUserById(string id, string includeProperties = "");
        UserDto GetUserByEmail(string email, string includeProperties = "");
        UserDto GetUserByUserName(string userName, string includeProperties = "");
        UserDto GetLoggedInUser(ClaimsPrincipal user);
    }
}