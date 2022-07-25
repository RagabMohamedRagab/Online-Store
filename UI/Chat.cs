using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI
{
    public class Chat:Hub
    {
        public async Task SendMessage(string message, string userId)
        {
            await Clients.Clients(userId).SendAsync("ReceiveMessage", message, Context.ConnectionId);
            await Clients.Clients(Context.ConnectionId).SendAsync("OwnMessage", message.Trim());
        }
        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            Clients.All.SendAsync("OnlineUserList", connectionId);
            return base.OnConnectedAsync();
        }
        public async Task OnlineUsers()
        {
            var connectionId = Context.ConnectionId;
            await Clients.All.SendAsync("OnlineUserList", connectionId);
        }
    }
}
