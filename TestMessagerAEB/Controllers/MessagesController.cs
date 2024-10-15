using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TestMessagerAEB.Data;
using TestMessagerAEB.Hubs;


namespace TestMessagerAEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        readonly AppDbContext context;
        readonly IHubContext<MessageHub> hubContext;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Message message)
        {
            if (string.IsNullOrEmpty(message.Text) || message.Text.Length > 128)
            {
                return BadRequest("Not correct message");
            }

            message.Time = DateTime.Now;
            context.messages.Add(message);
            await context.SaveChangesAsync();

            await hubContext.Clients.All.SendAsync("ReceiveMessage", message);

            return Ok();
    }

        [HttpGet("{startTime}/{endTime}")]
        public async Task<IActionResult> Get(DateTime startTime, DateTime endTime)
        {
            var messages = await context.messages.Where(m => m.Time >= startTime && m.Time <= endTime).ToListAsync();

            return Ok(messages);

        }
    }

    

   
}
