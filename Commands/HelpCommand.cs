using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    /// <summary>
    /// Represents a command that displays all available commands in the command-line interface.
    /// </summary>
    /// <remarks>
    /// The help output lists each command by its name followed by its description.
    /// This allows users to discover what commands exist and what their purpose is.
    ///
    /// <para>Example output:</para>
    /// <code>
    /// Available commands:
    /// List: Lists all registered sensors.
    /// Remove: Removes the sensor with the specified Id.
    /// Help: Displays this help message.
    /// ...
    /// </code>
    /// </remarks>
    /// <param name="commands">
    /// A dictionary of all commands registered in the system, keyed by their command names.
    /// </param>
    /// 
    /// <author>malackapite</author>
    internal class HelpCommand(IDictionary<string, ICommand> commands) : ICommand
    {
        private readonly IDictionary<string, ICommand> commands = commands;

        /// <summary>
        /// Gets the name of the command as used in the command-line.
        /// </summary>
        public string Name => "Help";

        /// <summary>
        /// Gets a readable description of what the command does.
        /// </summary
        public string Description => "Displays this help message.";

        /// <summary>
        /// Executes the command by printing all available commands and their descriptions.
        /// </summary>
        /// <param name="args">Command-line arguments (ignored).</param>
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
