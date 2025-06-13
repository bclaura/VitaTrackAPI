using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitaTrackAPI.Models;

namespace VitaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : Controller
    {
        private readonly VitaTrackContext _context;

        public DoctorsController(VitaTrackContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
        {
            var doctors = await _context.Doctors
                .Include(d => d.User)
                .Select(d => new DoctorDto
                {
                    Id = d.Id,
                    LastName = d.User.LastName,
                    FullName = d.User.FirstName + " " + d.User.LastName,
                    HonorificTitle = d.HonorificTitle ?? "",
                    Gender = d.Gender ?? "M",
                    IsFavorite = d.IsFavorite ?? false,
                    Bio = d.Bio ?? "",
                    AvailabilityHours = d.AvailabilityHours ?? "",
                    ClinicAddress = d.ClinicAddress ?? "",
                    Specialization = d.Specialization ?? "",
                    ProfilePictureBase64 = d.ProfilePictureBase64 ?? ""
                })
                .ToListAsync();

            return Ok(doctors);
        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<ActionResult<Doctor>> AddDoctor([FromBody] DoctorCreateDto doctorDto)
        {
            var doctor = new Doctor
            {
                UserId = doctorDto.UserId,
                HonorificTitle = doctorDto.HonorificTitle,
                Gender = doctorDto.Gender,
                IsFavorite = doctorDto.IsFavorite,
                Bio = doctorDto.Bio,
                AvailabilityHours = doctorDto.AvailabilityHours,
                ClinicAddress = doctorDto.ClinicAddress,
                ProfilePictureBase64 = doctorDto.ProfilePictureBase64
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDoctors), new { id = doctor.Id }, doctor);
        }

        // POST: api/Doctors/{doctorId}/SetProfilePicture
        [HttpPost("{doctorId}/SetProfilePicture")]
        public async Task<IActionResult> SetProfilePicture(int doctorId, [FromBody] string base64Image)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            doctor.ProfilePictureBase64 = base64Image;
            await _context.SaveChangesAsync();

            return Ok("Profile picture updated successfully");
        }
    }
}

public class DoctorDto
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string HonorificTitle { get; set; }
    public string Gender { get; set; }
    public bool IsFavorite { get; set; }
    public string Bio { get; set; }
    public string AvailabilityHours { get; set; }
    public string ClinicAddress { get; set; }
    public string ProfilePictureBase64 { get; set; }
    public string Specialization {  get; set; }
}

public class DoctorCreateDto
{
    public int UserId { get; set; }
    public string HonorificTitle { get; set; }
    public string Gender { get; set; }
    public bool IsFavorite { get; set; }
    public string Bio { get; set; }
    public string AvailabilityHours { get; set; }
    public string ClinicAddress { get; set; }
    public string ProfilePictureBase64 { get; set; }
}

