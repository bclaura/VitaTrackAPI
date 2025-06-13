using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using VitaTrackAPI.Models;

namespace VitaTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly VitaTrackContext _context;

        public UsersController(VitaTrackContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/Users/Register")]
        public async Task<ActionResult<UserDto>> Register(UserRegistrationDto registrationDto)
        {
            // Verificăm dacă emailul este deja folosit
            if (await _context.Users.AnyAsync(u => u.Email == registrationDto.Email))
            {
                return Conflict("A user with this email already exists.");
            }

            // Creăm noul user
            var user = new User
            {
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                Password = registrationDto.Password, // În real life, hash!
                MobileNumber = registrationDto.MobileNumber,
                DateOfBirth = registrationDto.DateOfBirth,
                Role = "patient",
                CreatedAt = DateTime.UtcNow
            };

            // Calculăm vârsta
            int age = DateTime.Now.Year - user.DateOfBirth.Year;
            if (DateTime.Now.Month < user.DateOfBirth.Month ||
                (DateTime.Now.Month == user.DateOfBirth.Month && DateTime.Now.Day < user.DateOfBirth.Day))
            {
                age--;
            }

            // Salvăm userul în DB
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // user.Id devine disponibil

            // Generăm CNP temporar random
            string tempCnp = GenerateRandomCnp();

            // Creăm pacientul asociat
            var patient = new Patient
            {
                UserId = user.Id,
                Email = user.Email,
                Age = age,
                Cnp = tempCnp,
                AdressStreet = null,
                AdressCity = null,
                AdressCounty = null,
                PhoneNumber = null,
                Occupation = null,
                Workplace = null
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            // Pregătim DTO-ul pentru răspuns
            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
                DateOfBirth = user.DateOfBirth,
                Role = user.Role!,
                CreatedAt = user.CreatedAt
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
        }



        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users/Login
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(UserLoginDto loginDto)
        {
            // Find user by email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            // Check if user exists and password matches
            if (user == null || user.Password != loginDto.Password) // In real app, compare hashed passwords
            {
                return Unauthorized("Invalid email or password");
            }

            // Return user data (would return a token in a real app)
            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                ProfilePictureBase64 = user.ProfilePictureBase64
            };

            return Ok(userDto);
        }

        [HttpPut("{userId}/ProfilePicture")]
        public async Task<IActionResult> UpdateProfilePicture(int userId, [FromBody] string base64String)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Salvăm imaginea ca Base64
            user.ProfilePictureBase64 = base64String.Trim('"');
            await _context.SaveChangesAsync();

            return Ok("Profile picture updated successfully");
        }

        // GET /api/users/by-email?email=example@example.com
        [HttpGet("by-email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        private string GenerateRandomCnp()
        {
            var random = new Random();
            var cnpBuilder = new StringBuilder();

            for (int i = 0; i < 13; i++)
            {
                cnpBuilder.Append(random.Next(0, 10)); // cifre între 0 și 9
            }

            return cnpBuilder.ToString();
        }

    }

    public class UserRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber {  get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string MobileNumber { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; } = "";
        public DateTime? CreatedAt { get; set; }
        public string ProfilePictureBase64 { get; set; }
    }

    public class PatientRegDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? AdressStreet { get; set; }
        public string? AdressCity { get; set; }
        public string? AdressCounty { get; set; }
        public string? Occupation { get; set; }
        public string? Workplace { get; set; }
    }
}
