using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.SignalR;

namespace CoreMVC5_UsedBookProject.Models
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var createdAt = DateTime.Now;
            var content = $"{user} 於 {createdAt.ToString("yyyy-MM-dd HH:mm:ss")} 說：{message}";

            await Clients.All.SendAsync("ReceiveMessage", content);
        }
    }
}
