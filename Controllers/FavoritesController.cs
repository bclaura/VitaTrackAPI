using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitaTrackAPI.Models;

namespace VitaTrack.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/favorites")]
    public class FavoritesController : ControllerBase
    {
        private readonly VitaTrackContext _context;

        public FavoritesController(VitaTrackContext context)
        {
            _context = context;
        }

        // GET /api/users/{userId}/favorites
        [HttpGet]
        public async Task<IActionResult> GetFavorites(int userId)
        {
            var favorites = await _context.UserFavorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Doctor)
                .ThenInclude(d => d.User)
                .Select(f => new DoctorDto
                {
                    Id = f.Doctor.Id,
                    LastName = f.Doctor.User.LastName,
                    FullName = f.Doctor.User.FirstName + " " + f.Doctor.User.LastName,
                    Gender = f.Doctor.Gender,
                    HonorificTitle = f.Doctor.HonorificTitle,
                    Bio = f.Doctor.Bio,
                    AvailabilityHours = f.Doctor.AvailabilityHours,
                    ClinicAddress = f.Doctor.ClinicAddress,
                    ProfilePictureBase64 = f.Doctor.ProfilePictureBase64,
                    Specialization = f.Doctor.Specialization
                })
                .ToListAsync();

            return Ok(favorites);
        }

        // POST /api/users/{userId}/favorites/{doctorId}
        [HttpPost("{doctorId}")]
        public async Task<IActionResult> AddFavorite(int userId, int doctorId)
        {
            if (await _context.UserFavorites.AnyAsync(f => f.UserId == userId && f.DoctorId == doctorId))
                return Conflict("Doctor already in favorites.");

            var favorite = new UserFavorite
            {
                UserId = userId,
                DoctorId = doctorId
            };

            _context.UserFavorites.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok("Doctor added to favorites.");
        }

        // DELETE /api/users/{userId}/favorites/{doctorId}
        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> RemoveFavorite(int userId, int doctorId)
        {
            var favorite = await _context.UserFavorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.DoctorId == doctorId);

            if (favorite == null)
                return NotFound("Favorite not found.");

            _context.UserFavorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return Ok("Doctor removed from favorites.");
        }
    }
}