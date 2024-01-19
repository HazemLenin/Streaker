using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Streaker.DAL.Dtos.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Streaker.Core.Domains;

namespace Streaker.DAL.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public UserDto GetUserByEmail(string email, string includeProperties = "")
        {
            var users = _userManager.Users;
            foreach (var property in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                users = users.Include(property);

            var user = users.FirstOrDefault(u => u.Email == email);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public UserDto GetUserById(string id, string includeProperties = "")
        {
            var users = _userManager.Users;
            foreach (var property in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                users = users.Include(property);

            var user = users.FirstOrDefault(u => u.Id == id);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public UserDto GetUserByUserName(string userName, string includeProperties = "")
        {
            var users = _userManager.Users;
            foreach (var property in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                users = users.Include(property);

            var user = users.FirstOrDefault(u => u.UserName == userName);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public UserDto GetLoggedInUser(ClaimsPrincipal user)
        {
            var actualUser = _userManager.GetUserAsync(user).Result;

            var userDto = _mapper.Map<UserDto>(actualUser);
            return userDto;
        }
    }
}