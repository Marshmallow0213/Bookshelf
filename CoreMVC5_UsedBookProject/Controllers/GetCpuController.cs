using CoreMVC5_UsedBookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class GetCpuController : Controller
    {
        private readonly IHubContext<CpuHub> _hubContext;

        public GetCpuController(IHubContext<CpuHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult CPUView()
        {
            return View();
        }

        public async Task<IActionResult> StartMonitoring()
        {
            while (true)
            {
                float cpuUsage = GetCpuUsage();
                await _hubContext.Clients.All.SendAsync("UpdateCpuUsage", cpuUsage);

                float memoryUsage = GetMemoryUsage();
                await _hubContext.Clients.All.SendAsync("UpdateMemoryUsage", memoryUsage);

                await Task.Delay(1000);
            }
        }

        private float GetCpuUsage()
        {
            using (Process process = Process.GetCurrentProcess())
            {
                TimeSpan totalProcessorTime = process.TotalProcessorTime;
                TimeSpan systemProcessorTime = TimeSpan.Zero;

                using (Process p = Process.GetCurrentProcess())
                {
                    p.Refresh();
                    systemProcessorTime = p.TotalProcessorTime;
                }

                float cpuUsage = (float)(totalProcessorTime.TotalMilliseconds / systemProcessorTime.TotalMilliseconds * 100.0);
                return cpuUsage;
            }
        }

        private float GetMemoryUsage()
        {
            using (Process process = Process.GetCurrentProcess())
            {
                long memoryUsed = process.PrivateMemorySize64;
                float memoryUsage = (float)memoryUsed / (1024 * 1024);
                return memoryUsage;
            }
        }
    }
}
