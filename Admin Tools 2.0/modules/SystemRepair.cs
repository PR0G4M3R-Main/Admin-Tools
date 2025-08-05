using System;
using System.Diagnostics;

namespace AdminTools
{
    public static class SystemRepair
    {
        public static void Run()
        {
            Console.WriteLine("Repairing system files... (this may take a while)");
            RunCommand("sfc", "/scannow");

            Console.WriteLine("\nRepairing system image...");
            RunCommand("Dism", "/Online /Cleanup-Image /RestoreHealth");

            Console.WriteLine("\nRepairing Disk... (restart required)");
            RunCommand("cmd", "/c echo Y | chkdsk C: /f /r");
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