using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitaTrackAPI.Models;

namespace VitaTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EcgSignalController : ControllerBase
    {
        private readonly VitaTrackContext _context;

        public EcgSignalController(VitaTrackContext context)
        {
            _context = context;
        }


        // GET: api/ecgsignal/patient/3
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<EcgSignal>>> GetSignalsByPatient(int patientId)
        {
            var signals = await _context.EcgSignals
                .Where(e => e.PatientId == patientId)
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync();

            return Ok(signals);
        }



        // POST: api/ecgsignal
        [HttpPost]
        public async Task<IActionResult> PostSignal([FromBody] EcgSignalDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Signal))
                return BadRequest("Signal is empty.");

            var ecgSignal = new EcgSignal
            {
                PatientId = dto.PatientId,
                Signal = dto.Signal,
                Timestamp = DateTime.Now.ToUniversalTime() // Asigurăm că timestampul este în UTC
            };

            _context.EcgSignals.Add(ecgSignal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSignalsByPatient), new { patientId = dto.PatientId }, ecgSignal);
        }

        // GET: api/ecgsignal/patient/{patientId}/summaries
        [HttpGet("patient/{patientId}/summaries")]
        public async Task<ActionResult<IEnumerable<SignalSummaryDto>>> GetSignalSummariesByPatient(int patientId)
        {
            var summaries = await _context.EcgSignals
                .Where(e => e.PatientId == patientId)
                .OrderByDescending(e => e.Timestamp)
                .Select(e => new SignalSummaryDto
                {
                    Id = e.Id,
                    PatientId = e.PatientId,
                    Timestamp = (DateTime)e.Timestamp
                })
                .ToListAsync();

            return Ok(summaries);
        }




        //GET /api/ecgsignals/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEcgSignalById(int id)
        {
            var signal = await _context.EcgSignals.FindAsync(id);
            if (signal == null)
                return NotFound();

            return Ok(signal);
        }
    }

    public class EcgSignalDto
    {
        public int PatientId { get; set; }
        public string Signal { get; set; } = string.Empty;
    }

    public class SignalSummaryDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
