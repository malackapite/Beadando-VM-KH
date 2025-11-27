using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    internal class ExitCommand : ICommand
    {
        public string Name => "Exit";
        public string Description => "Exits the application.";
        public void Execute(string[] args)
        {
            Console.WriteLine("Exiting application...");
            Environment.Exit(0);
        }
    }
}
