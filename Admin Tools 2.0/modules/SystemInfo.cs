using System;
using System.Diagnostics;

namespace AdminTools
{
    public static class SystemInfo
    {
        public static void Run()
        {
            Console.WriteLine("[Battery]");
            string reportPath = Environment.GetEnvironmentVariable("TEMP") + "\\battery-report.html";
            RunCommand("powercfg", $"/batteryreport /output \"{reportPath}\"");
            Console.WriteLine($"Battery report saved to: {reportPath}");

            Console.WriteLine("\n[Memory Modules]");
            RunPowerShell("Get-CimInstance Win32_PhysicalMemory | Format-Table BankLabel, Capacity, Speed, Manufacturer");

            Console.WriteLine("\n[Active Processes]");
            RunPowerShell("Get-Process | Sort-Object CPU -Descending | Select-Object Name, CPU, ID | Format-Table -AutoSize");

            Console.WriteLine("\n[Graphics Pipeline]");
            RunPowerShell("Get-CimInstance Win32_VideoController | Format-Table Name, DriverVersion, VideoModeDescription, CurrentRefreshRate");

            Console.WriteLine("\n[Startup Programs]");
            RunPowerShell("Get-CimInstance Win32_StartupCommand | Format-Table Name, Command, Location");
        }

        private static void RunPowerShell(string command)
        {
            RunCommand("powershell", $"-Command \"{command}\"");
        }

        private static void RunCommand(string fileName, string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process? process = Process.Start(psi))
            {
                if (process == null)
                {
                    Console.WriteLine($"Failed to start process: {psi.FileName}");
                    return;
                }

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(output);
            }
        }
    }
}