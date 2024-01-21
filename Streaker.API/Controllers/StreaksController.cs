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
                    StatusCode = 403,
                };

            return Ok(new ApiResponse<StreakDetailsDto>()
            {
                Data = streak,
            });
        }

        //// POST: api/Streaks
        //[HttpPost]
        //public ActionResult<ApiResponse<string>> PostStreak() { }

        //// PUT: api/Streaks/{id}
        //[HttpPut("{id}")]
        //public ActionResult PutStreak() { }

        //// DELETE: api/Streaks/{id}
        //[HttpPut("{id}")]
        //public ActionResult DeleteStreak() { }
    }
}
