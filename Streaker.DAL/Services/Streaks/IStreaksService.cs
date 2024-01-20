using Streaker.DAL.Dtos.Streaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Services.Streaks
{
    public interface IStreaksService
    {
        Task<IEnumerable<StreakDto>> GetUserStreaksAsync(string userId);
        Task<bool> CheckUserAuthorityAsync(string userId, string streakId);
        Task<bool> CheckExistsAsync(string streakId);
        Task<StreakDetailsDto> GetStreakDetailsAsync(string streakId);
    }
}
