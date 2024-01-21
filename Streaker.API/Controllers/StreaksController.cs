using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streaker.API.Responses;
using Streaker.DAL.Dtos.Streaks;
using Streaker.DAL.Services.Streaks;
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

        //// GET: api/Streaks/{id}
        //[HttpGet("{id}")]
        //public ActionResult<ApiResponse<StreakDetailsDto>> GetStreak(string id) { }

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
