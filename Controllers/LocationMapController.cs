using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using VitaTrackAPI.Models;

namespace VitaTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationMapController : ControllerBase
    {
        private readonly VitaTrackContext _context;

        public LocationMapController(VitaTrackContext context)
        {
            _context = context;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetLatestLocation(int patientId)
        {
            var latestLocation = await _context.LocationMaps
                .Where(l => l.PatientId == patientId)
                .OrderByDescending(l => l.RecordedAt)
                .FirstOrDefaultAsync();

            if (latestLocation == null)
                return NotFound(new { message = "No location found for this patient." });

            return Ok(latestLocation);
        }

        [HttpPost]
        public async Task<IActionResult> PostLocation([FromBody] LocationMapDto dto)
        {
            var location = new LocationMap
            {
                PatientId = dto.PatientId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                RecordedAt = DateTime.UtcNow.AddHours(3)
            };

            _context.LocationMaps.Add(location);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Location saved successfully." });
        }


    }

    public class LocationMapDto
    {
        public int PatientId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }


}
