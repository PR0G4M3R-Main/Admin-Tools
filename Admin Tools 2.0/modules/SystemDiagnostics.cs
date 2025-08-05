using System;
using System.Diagnostics;

namespace AdminTools
{
    public static class SystemDiagnostics
    {
        public static void Run()
        {
            Console.WriteLine("Clearing previous CBS log entries...");
            RunCommand("cmd", "/c del /f /q %windir%\\Logs\\CBS\\CBS.log");

            Console.WriteLine("\nChecking system files... (this may take a while)");
            RunCommand("sfc", "/verifyonly");

            Console.WriteLine("\nChecking system image...");
            RunCommand("Dism", "/Online /Cleanup-Image /ScanHealth");

            Console.WriteLine("\nScanning disk status...");
            RunCommand("chkdsk", "C:");
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