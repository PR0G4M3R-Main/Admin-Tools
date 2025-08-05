using System;
using System.Diagnostics;

namespace AdminTools
{
    public static class SystemBoost
    {
        public static void Run()
        {
            Console.WriteLine("Switching to High Performance mode...");
            RunCommand("powercfg", "/setactive SCHEME_MIN");

            Console.WriteLine("Setting Processor Performance Boost Mode to Aggressive...");
            RunCommand("powercfg", "-setacvalueindex SCHEME_MIN SUB_PROCESSOR PERFBOOSTMODE 2");
            RunCommand("powercfg", "-setdcvalueindex SCHEME_MIN SUB_PROCESSOR PERFBOOSTMODE 2");

            Console.WriteLine("Applying updated power settings...");
            RunCommand("powercfg", "/S SCHEME_MIN");

            Console.WriteLine("Disabling Windows Defender scheduled tasks...");
            DisableTask(@"\Microsoft\Windows\Windows Defender\Windows Defender Scheduled Scan");
            DisableTask(@"\Microsoft\Windows\Windows Defender\Windows Defender Verification");
            DisableTask(@"\Microsoft\Windows\Windows Defender\Windows Defender Cache Maintenance");
            DisableTask(@"\Microsoft\Windows\Windows Defender\Windows Defender Cleanup");

            Console.WriteLine("All Defender tasks disabled and system fully optimized.");
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

        private static void DisableTask(string taskName)
        {
            RunCommand("schtasks", $"/Change /TN \"{taskName}\" /Disable");
        }
    }
}