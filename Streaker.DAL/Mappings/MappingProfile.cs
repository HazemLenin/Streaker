using AutoMapper;
using Streaker.DAL.Dtos.Users;
using Streaker.Core;
using Streaker.Core.Domains;
using Streaker.DAL.Dtos.Streaks;

namespace Streaker.DAL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ReverseMap();

            CreateMap<StreakDetailsDto, Streak>()
                .ReverseMap();

            CreateMap<StreakAddDto, Streak>()
                .ReverseMap();

            CreateMap<StreakUpdateDto, Streak>()
                .ReverseMap();
        }
    }
}