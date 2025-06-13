using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitaTrackAPI.Models;

namespace VitaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly VitaTrackContext _context;

        public MedicalHistoryController(VitaTrackContext context)
        {
            _context = context;
        }

        // GET: api/MedicalHistory/patient/5
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<MedicalHistory>>> GetMedicalHistoryByPatient(int patientId)
        {
            var history = await _context.MedicalHistories
                .Where(h => h.PatientId == patientId)
                .ToListAsync();

            return Ok(history);
        }

        // POST: api/MedicalHistory
        [HttpPost]
        public async Task<ActionResult> CreateMedicalHistory(MedicalHistoryDto dto)
        {
            var newEntry = new MedicalHistory
            {
                PatientId = dto.PatientId,
                History = dto.History,
                Allergies = dto.Allergies,
                CardiologyConsultations = dto.CardiologyConsultations,
                CreatedAt = dto.CreatedAt,
            };

            _context.MedicalHistories.Add(newEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicalHistoryByPatient), new { patientId = dto.PatientId }, newEntry);
        }
    }

    public class MedicalHistoryDto
    {
        public int PatientId { get; set; }
        public string? History { get; set; }
        public string? Allergies { get; set; }
        public string? CardiologyConsultations { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
