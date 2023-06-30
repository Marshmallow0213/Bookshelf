// CpuHub.cs
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CoreMVC5_UsedBookProject
{
    public class CpuHub : Hub
    {
        public async Task SendMessage(float cpuUsage)
        {
            await Clients.All.SendAsync("UpdateCpuUsage", cpuUsage);
        }
    }
}
