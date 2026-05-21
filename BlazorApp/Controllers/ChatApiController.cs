using BlazorApp.Data;
using BlazorApp.Services;
using BlazorApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Controllers
{
        [ApiController]
        [Route("api/chat")]
        public class ChatApiController : ControllerBase
        {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ChatApiController(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Text) || string.IsNullOrWhiteSpace(request.ReceiverId))
                return BadRequest();

            using var db = await _dbFactory.CreateDbContextAsync();

            var newMessage = new ChatMessage
            {
                SenderId = request.SenderId,
                SenderName = request.SenderName,
                ReceiverId = request.ReceiverId, 
                MessageText = request.Text,
                SentAt = DateTime.UtcNow
            };

            db.ChatMessages.Add(newMessage);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetMessageCount([FromQuery] string user1, [FromQuery] string user2)
        {
            if (string.IsNullOrEmpty(user1) || string.IsNullOrEmpty(user2))
                return BadRequest(0);

            using var db = await _dbFactory.CreateDbContextAsync();

            int count = await db.ChatMessages
                .CountAsync(m => (m.SenderId == user1 && m.ReceiverId == user2)
                              || (m.SenderId == user2 && m.ReceiverId == user1));

            return Ok(count);
        }
    }

    public class ChatMessageRequest
    {
        public string SenderId { get; set; } = "";
        public string SenderName { get; set; } = "";
        public string ReceiverId { get; set; } = ""; 
        public string Text { get; set; } = "";
    }
}
