using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CoreMVC5_UsedBookProject.Controllers
{
    public class GetCpuController : Controller
    {
        public IActionResult CPUView()
        {
            return View();
        }
        public IActionResult GetCpuUsage()
        {
            var cpuUsage = FetchCpuUsage();
            return Json(new { cpuUsage });
        }

        private float FetchCpuUsage()
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "wmic",
                    Arguments = "cpu get loadpercentage",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            var cpuUsage = 0;
            var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length >= 2)
            {
                if (int.TryParse(lines[1], out var usage))
                {
                    cpuUsage = usage;
                }
            }

            return cpuUsage;
        }

        public IActionResult Memory()
        {
            return View();
        }

        public IActionResult GetMemoryUsage()
        {
            var memoryUsage = FetchMemoryUsage();
            return Json(new { memoryUsage });
        }

        private float FetchMemoryUsage()
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "wmic",
                    Arguments = "OS get FreePhysicalMemory, TotalVisibleMemorySize /Value",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            var freeMemory = 0ul;
            var totalMemory = 0ul;
            foreach (var line in output.Split("\n"))
            {
                if (line.StartsWith("FreePhysicalMemory=") && ulong.TryParse(line.Substring(19), out freeMemory))
                {
                    continue;
                }

                if (line.StartsWith("TotalVisibleMemorySize=") && ulong.TryParse(line.Substring(23), out totalMemory))
                {
                    break;
                }
            }

            var memoryUsage = ((float)(totalMemory - freeMemory) / totalMemory) * 100;
            return memoryUsage;
        }
    }
}
