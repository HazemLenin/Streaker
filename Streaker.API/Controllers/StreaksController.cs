using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streaker.API.Responses;
using Streaker.DAL.Dtos.Streaks;

namespace Streaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StreaksController : ControllerBase
    {
        // GET: api/Streaks
        [HttpGet]
        public ActionResult<ApiResponse<List<StreakDto>>> GetStreaks() { }

        // GET: api/Streaks/{id}
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<StreakDetailsDto>> GetStreak(string id) { }

        // POST: api/Streaks
        [HttpPost]
        public ActionResult<ApiResponse<string>> PostStreak() { }

        // PUT: api/Streaks/{id}
        [HttpPut("{id}")]
        public ActionResult PutStreak() { }

        // DELETE: api/Streaks/{id}
        [HttpPut("{id}")]
        public ActionResult DeleteStreak() { }
    }
}
