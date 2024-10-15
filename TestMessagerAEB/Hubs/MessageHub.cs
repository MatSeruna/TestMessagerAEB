using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using TestMessagerAEB.Data;

namespace TestMessagerAEB.Hubs
{
    public class MessageHub : Hub
    {
        private readonly AppDbContext _context;

        public MessageHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string message)
        {
            var msg = new Message { Text = message};
            _context.messages.Add(msg);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendToClients(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("New client has been connected");
            return base.OnConnectedAsync(); 
        }
    }
}
