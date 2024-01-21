using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streaker.API.Responses;
using Streaker.DAL.Dtos.Streaks;
using Streaker.DAL.Services.Streaks;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace Streaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StreaksController(IStreaksService streaksService) : ControllerBase
    {
        private readonly IStreaksService _streaksService = streaksService;

        // GET: api/Streaks
        [HttpGet]
        public async Task<ActionResult<ApiPaginatedResponse<List<StreakDto>>>> GetStreaks(int pageNumber = 1, int pageSize = 10)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var streaks = await _streaksService.GetUserPaginatedStreaksAsync(userId, pageNumber, pageSize);
            return Ok(new ApiPaginatedResponse<StreakDto>(streaks));
        }

        // GET: api/Streaks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<StreakDetailsDto>>> GetStreak(string id)
        {
            var streak = await _streaksService.GetStreakDetailsAsync(id);
            if (streak == null)
                return NotFound(new ApiResponse()
                {
                    Errors = new()
                    {
                        [""] = ["Streak cannot be found."]
                    }
                });

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userCanAccess = await _streaksService.CheckUserAuthorityAsync(userId, id);
            if (!userCanAccess)
                return new ObjectResult(new ApiResponse()
                {
                    Errors = new()
                    {
                        [""] = ["You cannot access this streak."]
                    }
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                };

            return Ok(new ApiResponse<StreakDetailsDto>(streak));
        }

        // POST: api/Streaks
        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> PostStreak(StreakAddDto streakAddDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var streakId = await _streaksService.AddStreakAsync(streakAddDto, userId);
            return new ObjectResult(new ApiResponse<string>(streakId))
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        // PUT: api/Streaks/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutStreak(string id, StreakUpdateDto streakUpdateDto)
        {
            var streakExists = await _streaksService.CheckExistsAsync(id);
            if (!streakExists)
                return NotFound(new ApiResponse()
                {
                    Errors = new()
                    {
                        [""] = ["Streak cannot be found."]
                    }
                });

            await _streaksService.UpdateStreakAsync(streakUpdateDto);
            return Created();
        }

        // DELETE: api/Streaks/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteStreak(string id)
        {
            var streakExists = await _streaksService.CheckExistsAsync(id);
            if (!streakExists)
                return NotFound(new ApiResponse()
                {
                    Errors = new()
                    {
                        [""] = ["Streak cannot be found."]
                    }
                });

            await _streaksService.DeleteStreakAsync(id);
            return new ObjectResult(new ApiResponse())
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
    }
}
