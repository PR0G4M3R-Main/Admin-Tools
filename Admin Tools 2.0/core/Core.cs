using System;
using System.Collections.Generic;
using AdminTools;

namespace Admin_Tools
{
    class Core
    {
        static Dictionary<string, Action> commandMap = new Dictionary<string, Action>
        {
            { "NetworkInfo", NetworkInfo.Run },
            { "RigInfo", RigInfo.Run },
            { "SystemBackup", SystemBackup.Run },
            { "SystemBoost", SystemBoost.Run },
            { "SystemClean", SystemClean.Run },
            { "SystemDiagnostics", SystemDiagnostics.Run },
            { "SystemInfo", SystemInfo.Run },
            { "SystemRepair", SystemRepair.Run },
            { "commands", ListCommands }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("AdminShell — type a command or 'exit' to quit.");
            Console.WriteLine("Type 'commands' to list available options.");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                if (commandMap.TryGetValue(input, out var command))
                {
                    command.Invoke();
                }
                else
                {
                    Console.WriteLine("Unknown command. Type 'commands' to see available options.");
                }
            }
        }

        static void ListCommands()
        {
            Console.WriteLine("Available Commands:");
            foreach (var cmd in commandMap.Keys)
            {
                Console.WriteLine($" - {cmd}");
            }
        }
    }
}