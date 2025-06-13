using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitaTrackAPI.Models;

namespace VitaTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysicalActivityController : ControllerBase
    {
        private readonly VitaTrackContext _context;

        public PhysicalActivityController(VitaTrackContext context)
        {
            _context = context;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetActivities(int patientId)
        {
            var acts = await _context.PhysicalActivities
                .Where(a => a.PatientId == patientId)
                .ToListAsync();

            return Ok(acts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto dto)
        {
            var act = new PhysicalActivity
            {
                PatientId = dto.PatientId,
                ActivityType = dto.ActivityType,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Duration = dto.Duration
            };

            _context.PhysicalActivities.Add(act);
            await _context.SaveChangesAsync();

            return Ok(act);
        }
    }

    public class CreateActivityDto
    {
        public int PatientId { get; set; }
        public string ActivityType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; } // minute
    }
}
