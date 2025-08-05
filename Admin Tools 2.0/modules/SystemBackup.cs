using System;
using System.Diagnostics;

namespace AdminTools
{
    public static class SystemBackup
    {
        public static void Run()
        {
            Console.WriteLine("Creating System Restore Point...");
            RunPowerShell("Checkpoint-Computer -Description 'SystemBackup Tool' -RestorePointType 'MODIFY_SETTINGS'");

            Console.WriteLine("\nBacking up key folders...");
            string userDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string userPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string backupRoot = Environment.GetEnvironmentVariable("SystemDrive") + "\\Backup";

            RunCommand("xcopy", $"\"{userDocuments}\" \"{backupRoot}\\Documents\" /E /I /Y");
            RunCommand("xcopy", $"\"{userPictures}\" \"{backupRoot}\\Pictures\" /E /I /Y");

            Console.WriteLine("\nBackup complete. Folders saved to " + backupRoot);
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