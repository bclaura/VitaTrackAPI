using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitaTrackAPI.Models;

namespace VitaTrackAPI.Controllers
{
    [ApiController]
    [Route("api/messages/")]
    public class MessagesController : ControllerBase
    {
        private readonly VitaTrackContext _context;

        public MessagesController(VitaTrackContext context)
        {
            _context = context;
        }

        [HttpGet("conversation")]
        public async Task<IActionResult> GetConversation([FromQuery] int userId, [FromQuery] int doctorId)
        {
            var messages = await _context.Messages
                .Where(m =>
                    (m.SenderId == userId && m.ReceiverId == doctorId) ||
                    (m.SenderId == doctorId && m.ReceiverId == userId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Message))
                return BadRequest("Message cannot be empty.");

            var newMessage = new Message
            {
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Message1 = dto.Message,
                SentAt = DateTime.Now.ToUniversalTime(),
                IsRead = false
            };

            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            return Ok(newMessage);
        }

        [HttpPut("mark-as-read")]
        public async Task<IActionResult> MarkMessagesAsRead(int userId, int doctorId)
        {
            var unreadMessages = await _context.Messages
                .Where(m => m.SenderId == userId && m.ReceiverId == doctorId)
                .ToListAsync();

            foreach (var msg in unreadMessages)
                msg.IsRead = true;

            await _context.SaveChangesAsync();
            return NoContent();
        }


    }

    public class SendMessageDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
