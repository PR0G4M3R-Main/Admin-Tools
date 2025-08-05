using System;
using System.Diagnostics;

namespace AdminTools
{
    public static class RigInfo
    {
        public static void Run()
        {
            Console.WriteLine("[CPU]");
            RunPowerShell("Get-CimInstance Win32_Processor | Format-Table Name, MaxClockSpeed, NumberOfCores");

            Console.WriteLine("\n[GPU]");
            RunPowerShell("Get-CimInstance Win32_VideoController | Format-Table Name, DriverVersion");

            Console.WriteLine("\n[RAM]");
            RunPowerShell("Get-CimInstance Win32_PhysicalMemory | Select-Object Manufacturer, Capacity, Speed | Format-Table");

            Console.WriteLine("[Installed RAM Total]");
            RunPowerShell("(Get-CimInstance Win32_ComputerSystem).TotalPhysicalMemory / 1GB");

            Console.WriteLine("\n[Motherboard]");
            RunPowerShell("Get-CimInstance Win32_BaseBoard | Format-Table Manufacturer, Product, Version");

            Console.WriteLine("\n[Storage]");
            RunPowerShell("Get-CimInstance Win32_DiskDrive | Format-Table Model, Size, InterfaceType");

            Console.WriteLine("\n[Monitor(s)]");
            RunPowerShell("Get-CimInstance Win32_DesktopMonitor | Select Name, ScreenWidth, ScreenHeight | Format-Table");

            Console.WriteLine("\n[Operating System]");
            RunPowerShell("Get-CimInstance Win32_OperatingSystem | Format-Table Caption, Version, BuildNumber");

            Console.WriteLine("\n[Network]");
            RunPowerShell("Get-CimInstance Win32_NetworkAdapter | Where-Object { $_.NetConnectionStatus -eq 2 } | Format-Table Name, MACAddress");

            Console.WriteLine("\n[Battery]");
            RunPowerShell("Get-CimInstance Win32_Battery | Format-Table Name, EstimatedChargeRemaining, BatteryStatus");

            Console.WriteLine("\n[BIOS]");
            RunPowerShell("Get-CimInstance Win32_BIOS | Format-Table Manufacturer, Version, ReleaseDate");
        }

        private static void RunPowerShell(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-Command \"{command}\"",
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