using System;
using System.Diagnostics;

namespace AdminTools
{
    public static class NetworkInfo
    {
        public static void Run()
        {
            Console.WriteLine("[Open Ports & Connections]");
            RunCommand("netstat", "-ano");

            Console.WriteLine("\n[IP & Adapter Info]");
            RunCommand("ipconfig", "/all");

            Console.WriteLine("\n[Ping Test - Google DNS]");
            RunCommand("ping", "8.8.8.8 -n 4");

            Console.WriteLine("\n[Traceroute - Google DNS]");
            RunCommand("tracert", "8.8.8.8");

            Console.WriteLine("\n[DNS Lookup - Microsoft Domain]");
            RunCommand("nslookup", "www.microsoft.com");

            Console.WriteLine("\n[Wi-Fi SSID (if applicable)]");
            RunCommand("netsh", "wlan show interfaces");

            Console.WriteLine("\n[Adapter List - Connected Only]");
            RunPowerShell("Get-CimInstance Win32_NetworkAdapter | Where-Object { $_.NetConnectionStatus -eq 2 } | Format-Table Name, MACAddress");
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

        private static void RunPowerShell(string command)
        {
            RunCommand("powershell", $"-Command \"{command}\"");
        }
    }
}