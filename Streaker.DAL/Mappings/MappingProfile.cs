using AutoMapper;
using Streaker.DAL.Dtos.Users;
using Streaker.Core;
using Streaker.Core.Domains;

namespace Streaker.DAL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ReverseMap();
        }
    }
}