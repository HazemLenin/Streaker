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
                return Forbid();

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

        // // PUT: api/Streaks/{id}
        // [HttpPut("{id}")]
        // public async Task<ActionResult> PutStreak(string id, StreakUpdateDto streakUpdateDto)
        // {
        //     var streakExists = await _streaksService.CheckExistsAsync(id);
        //     if (!streakExists)
        //         return NotFound(new ApiResponse()
        //         {
        //             Errors = new()
        //             {
        //                 [""] = ["Streak cannot be found."]
        //             }
        //         });

        //     var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //     var userCanAccess = await _streaksService.CheckUserAuthorityAsync(userId, id);
        //     if (!userCanAccess)
        //         return Forbid();

        //     await _streaksService.UpdateStreakAsync(id, streakUpdateDto);
        //     return Created();
        // }

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

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userCanAccess = await _streaksService.CheckUserAuthorityAsync(userId, id);
            if (!userCanAccess)
                return Forbid();

            await _streaksService.DeleteStreakAsync(id);
            return NoContent();
        }

        // GET: api/Streaks/GetCurrentMonthStreak/{id}
        [HttpGet("GetCurrentMonthStreak/{id}")]
        public async Task<ActionResult<ApiResponse>> GetCurrentMonthStreak(string id)
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

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userCanAccess = await _streaksService.CheckUserAuthorityAsync(userId, id);
            if (!userCanAccess)
                return Forbid();

            var commits = await _streaksService.GetCurrentMonthStreak(id);
            var commitedToday = await _streaksService.CommitedToday(id);

            return Ok(new ApiResponse<StreakCalendarDto>(new()
            {
                Commits = commits,
                CommitedToday = commitedToday
            }));
        }

        // POST: /api/Streaks/CommitToday/{id}
        [HttpPost("CommitToday/{id}")]
        public async Task<ActionResult<ApiResponse>> CommitToday(string id)
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

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userCanAccess = await _streaksService.CheckUserAuthorityAsync(userId, id);
            if (!userCanAccess)
                return Forbid();

            _streaksService.CommitStreak(id);
            return Ok();
        }
    }
}
