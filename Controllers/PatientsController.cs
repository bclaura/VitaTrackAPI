using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitaTrackAPI.Models;

namespace VitaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly VitaTrackContext _context;

        public PatientsController(VitaTrackContext context)
        {
            _context = context;
        }

        // GET: api/patients/byUserId/5
        [HttpGet("byUserId/{userId}")]
        public async Task<ActionResult<PatientDto>> GetPatientByUserId(int userId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
            if (patient == null)
                return NotFound();

            var dto = new PatientDto
            {
                Id = patient.Id,
                Age = (int)patient.Age,
                Cnp = patient.Cnp,
                UserId = patient.UserId,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                AdressStreet = patient.AdressStreet,
                AdressCity = patient.AdressCity,
                AdressCounty = patient.AdressCounty,
                Occupation = patient.Occupation,
                Workplace = patient.Workplace
            };

            return Ok(dto);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatient(PatientCreateDto dto)
        {
            var patient = new Patient
            {
                UserId = dto.UserId,
                Cnp = dto.Cnp,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                AdressStreet = dto.AdressStreet,
                AdressCity = dto.AdressCity,
                AdressCounty = dto.AdressCounty,
                Occupation = dto.Occupation,
                Workplace = dto.Workplace
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            var responseDto = new PatientDto
            {
                Id = patient.Id,
                Cnp = patient.Cnp,
                UserId = patient.UserId,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                AdressStreet = patient.AdressStreet,
                AdressCity = patient.AdressCity,
                AdressCounty = patient.AdressCounty,
                Occupation = patient.Occupation,
                Workplace = patient.Workplace
            };

            return CreatedAtAction(nameof(GetPatientByUserId), new { userId = patient.UserId }, responseDto);
        }

        // PUT: api/patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, PatientUpdateDto dto)
        {

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            // Actualizează câmpurile
            patient.Email = dto.Email;
            patient.Cnp = dto.Cnp;
            patient.PhoneNumber = dto.PhoneNumber;
            patient.AdressStreet = dto.AdressStreet;
            patient.AdressCity = dto.AdressCity;
            patient.AdressCounty = dto.AdressCounty;
            patient.Occupation = dto.Occupation;
            patient.Workplace = dto.Workplace;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class PatientDto
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string? Cnp { get; set; }
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AdressStreet { get; set; }
        public string? AdressCity { get; set; }
        public string? AdressCounty { get; set; }
        public string? Occupation { get; set; }
        public string? Workplace { get; set; }
    }

    public class PatientCreateDto
    {
        public int UserId { get; set; }
        public string? Cnp { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AdressStreet { get; set; }
        public string? AdressCity { get; set; }
        public string? AdressCounty { get; set; }
        public string? Occupation { get; set; }
        public string? Workplace { get; set; }
    }

    public class PatientUpdateDto
    {
        public string? Cnp {  get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AdressStreet { get; set; }
        public string? AdressCity { get; set; }
        public string? AdressCounty { get; set; }
        public string? Occupation { get; set; }
        public string? Workplace { get; set; }
    }
}
