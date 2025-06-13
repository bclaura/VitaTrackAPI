using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitaTrackAPI.Models;

namespace VitaTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ControllerBase
    {
        private readonly VitaTrackContext _context;

        public RecommendationsController(VitaTrackContext context)
        {
            _context = context;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetRecommendations(int patientId)
        {
            var recs = await _context.Recommendations
                .Where(r => r.PatientId == patientId)
                .ToListAsync();

            return Ok(recs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecommendation([FromBody] CreateRecommendationDto dto)
        {
            var rec = new Recommendation
            {
                PatientId = dto.PatientId,
                RecommendationType = dto.RecommendationType,
                DailyDuration = dto.DailyDuration,
                AdditionalInstructions = dto.AdditionalInstructions
            };

            _context.Recommendations.Add(rec);
            await _context.SaveChangesAsync();

            return Ok(rec);
        }
    }

    public class CreateRecommendationDto
    {
        public int PatientId { get; set; }
        public string RecommendationType { get; set; }
        public int DailyDuration { get; set; }
        public string? AdditionalInstructions { get; set; }
    }
}
