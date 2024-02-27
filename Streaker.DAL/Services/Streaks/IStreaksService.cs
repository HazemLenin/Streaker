using Streaker.DAL.Dtos.Streaks;
using Streaker.DAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Services.Streaks
{
    public interface IStreaksService
    {
        Task<PaginatedList<StreakDto>> GetUserPaginatedStreaksAsync(string userId, int pageNumber, int pageSize);
        Task<string> AddStreakAsync(StreakAddDto streakAddDto, string userId);
        Task<bool> CheckUserAuthorityAsync(string userId, string streakId);
        Task<bool> CheckExistsAsync(string streakId);
        Task<StreakDetailsDto> GetStreakDetailsAsync(string streakId);
        Task UpdateStreakAsync(string streakId, StreakUpdateDto streakUpdateDto);
        Task DeleteStreakAsync(string streakId);
        Task<List<int>> GetCurrentMonthStreak(string streakId);
        Task<Boolean> CommitedToday(string streakId);
        Task CommitStreak(string streakId);
    }
}
