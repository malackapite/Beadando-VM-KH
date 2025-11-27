using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    internal class HelpCommand(IDictionary<string, ICommand> commands) : ICommand
    {
        private readonly IDictionary<string, ICommand> commands = commands;

        public string Name => "Help";
        public string Description => "Displays this help message.";
        public void Execute(string[] args)
        {
            Console.WriteLine("Available commands:");
            foreach (var command in commands.Values)
            {
                Console.WriteLine($"{command.Name}: {command.Description}");
            }
        }
    }
}
