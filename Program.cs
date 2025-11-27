using Microsoft.Extensions.Configuration;
using SimpleLocalDB;
﻿using Beadando_VM_KH.Commands;
using Beadando_VM_KH.model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Beadando_VM_KH
{
    internal partial class Program
    {
        static readonly IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppDbContext.BasePath)
            .AddJsonFile("appconfig.json", false, true)
            .Build()
        ;
            
        static void Main()
        {
            CentralStation centralStation = new(config);
            Dictionary<string, ICommand> commands = [];

            commands["add"] = new AddCommand(centralStation);
            commands["remove"] = new RemoveCommand(centralStation);
            commands["list"] = new ListCommand(centralStation);
            commands["export"] = new ExportCommand(centralStation);
            commands["help"] = new HelpCommand(commands);
            commands["exit"] = new ExitCommand();
            while (true)
            {
                string command = Console.ReadLine()!;
                string[] commandParts = command.Split(' ');
                if (commands.TryGetValue(commandParts[0], out ICommand? value))
                {
                    value.Execute(commandParts[1..]);
                }
                else
                {
                    Console.WriteLine("Unknown command. Use 'help' to see available commands.");
                }
            }
        }
    }
}
