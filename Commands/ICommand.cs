using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    /// <summary>
    /// Interface for commands in the central station application.
    /// <author>malackapite</author>
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The name of the command, used by the user to invoke it ("List", "Add", etc. ).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A short description explaining what the command does. This is useful for help menus, documentation, or command listings.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Method that executes the command. It receives an array of arguments provided by the user.
        /// </summary>
        /// <param name="args">Array containing the arguments for the command.</param>
        void Execute(string[] args);
    }
}
