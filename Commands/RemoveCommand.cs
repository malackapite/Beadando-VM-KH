using Beadando_VM_KH.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    internal class RemoveCommand(CentralStation centralStation) : ICommand
    {
        private readonly CentralStation centralStation = centralStation;
        public string Name => "Remove";

        public string Description => "Removes the sensor with the specified Id.";

        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: remove <Id>");
                return;
            }
            Sensor? sensorToRemove = centralStation.RemoveSensor(args[0]);
            if (sensorToRemove != null)
            {
                Console.WriteLine($"Removed sensor with Id {sensorToRemove.Id}.");
            }
            else
            {
                Console.WriteLine($"No sensor found with Id {args[0]}.");
            }
        }
    }
}
