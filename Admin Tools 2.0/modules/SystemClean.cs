using System;
using System.Diagnostics;

namespace AdminTools
{
    public static class SystemClean
    {
        public static void Run()
        {
            Console.WriteLine("Clearing Microsoft Store cache...");
            RunCommand("wsreset.exe", "");

            Console.WriteLine("Waiting for Microsoft Store to open...");
            System.Threading.Thread.Sleep(5000);

            Console.WriteLine("Closing Microsoft Store...");
            RunCommand("taskkill", "/F /IM WinStore.App.exe");

            Console.WriteLine("Clearing Windows Update cache...");
            RunCommand("net", "stop wuauserv");
            RunCommand("net", "stop bits");
            RunCommand("cmd", "/c del /f /s /q %windir%\\SoftwareDistribution\\Download\\*.*");

            Console.WriteLine("Starting Windows Update services...");
            RunCommand("net", "start wuauserv");
            RunCommand("net", "start bits");

            Console.WriteLine("Flushing DNS cache...");
            RunCommand("ipconfig", "/flushdns");

            Console.WriteLine("Flushing ARP cache...");
            RunCommand("arp", "-d");

            Console.WriteLine("Resetting Winsock...");
            RunCommand("netsh", "winsock reset");

            Console.WriteLine("Cleaning temp files...");
            RunCommand("cmd", "/c for %F in (%TEMP%\\*.*) do del /f /q \"%F\"");

            Console.WriteLine("Cleaning Windows Update leftovers...");
            RunCommand("Dism", "/Online /Cleanup-Image /StartComponentCleanup");

            Console.WriteLine("Removing device driver packages...");
            RunCommand("cmd", "/c pnputil /enum-drivers > drivers.txt");
            RunCommand("cmd", "/c for /F \"tokens=1 delims=:\" %d in ('findstr /C:\"Published Name\" drivers.txt') do pnputil /delete-driver %d /uninstall /force");

            Console.WriteLine("Removing drivers.txt...");
            RunCommand("cmd", "/c del /f /q drivers.txt");

            Console.WriteLine("Clearing all temp folders...");
            RunCommand("cmd", "/c del /q/f/s \"%TEMP%\\*\"");
            RunCommand("cmd", "/c del /q/f/s \"%SystemRoot%\\Temp\\*\"");

            Console.WriteLine("All done! Reboot for best results.");
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