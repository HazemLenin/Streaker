using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Streaker.DAL.Dtos.Streaks;
using Streaker.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.DAL.Services.Streaks
{
    public class StreaksService(IUnitOfWork unitOfWork, IMapper mapper) : IStreaksService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> CheckExistsAsync(string streakId)
        {
            return await _unitOfWork.StreaksRepository.CheckExistsAsync(streakId);
        }

        public async Task<bool> CheckUserAuthorityAsync(string userId, string streakId)
        {
            return await _unitOfWork.StreaksRepository.GetAll().AnyAsync(e => e.ApplicationUserId == userId && e.Id == streakId);
        }

        public async Task<StreakDetailsDto> GetStreakDetailsAsync(string streakId)
        {
            // TODO: Select less cols
            var streak = await _unitOfWork.StreaksRepository.GetByIdAsync(streakId);
            return _mapper.Map<StreakDetailsDto>(streak);
        }

        public async Task<IEnumerable<StreakDto>> GetUserStreaksAsync(string userId)
        {
            var streaks = _unitOfWork.StreaksRepository.GetAll(s => s.ApplicationUserId == userId)
                .Select(s => new StreakDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category,
                    StreakCount = s.StreakCount,
                    TargetCount = s.TargetCount,
                    Created = s.Created
                });

            return await streaks.ToListAsync();
        }
    }
}
