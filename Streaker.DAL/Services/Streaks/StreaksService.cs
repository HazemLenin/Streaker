using Streaker.DAL.Dtos.Streaks;
using Streaker.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Services.Streaks
{
    public class StreaksService(IUnitOfWork unitOfWork) : IStreaksService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> CheckExistsAsync(string streakId)
        {
            return await _unitOfWork.StreaksRepository.CheckExistsAsync(streakId);
        }

        public Task<bool> CheckUserAuthorityAsync(string userId, string streakId)
        {
            throw new NotImplementedException();
        }

        public Task<StreakDetailsDto> GetStreakDetailsAsync(string streakId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StreakDto>> GetUserStreaksAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
