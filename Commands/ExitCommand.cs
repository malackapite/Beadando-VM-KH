using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    /// <summary>
    /// Represents a command that terminates the application.
    /// </summary>
    /// <remarks>
    /// When executed, the command prints a shutdown message and immediately stops the application process.
    ///
    /// <para>Example usage:</para>
    /// <code>
    /// exit
    /// Exiting application...
    /// </code>
    /// </remarks>
    /// <author>malackapite</author>
    internal class ExitCommand : ICommand
    {
        /// <summary>
        /// Gets the name of the command as used in the command-line.
        /// </summary>
        public string Name => "Exit";

        /// <summary>
        /// Gets a readable description of what the command does.
        /// </summary>
        public string Description => "Exits the application.";

        /// <summary>
        /// Executes the command and terminates the running application.
        /// </summary>
        /// <param name="args">Command-line arguments (ignored).</param>
        public void Execute(string[] args)
        {
            Console.WriteLine("Exiting application...");
            Environment.Exit(0);
        }
    }
}
