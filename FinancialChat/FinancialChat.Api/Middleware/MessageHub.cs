using FinancialChat.Application.Models;
using Microsoft.AspNetCore.SignalR;

namespace FinancialChat.Api.Middleware
{
    public class MessageHub : Hub
    {
        public async Task NewMessage(Message msg, string user)
        {
            await Clients.All.SendAsync("MessageReceived",msg, user);
        }
    }
}
