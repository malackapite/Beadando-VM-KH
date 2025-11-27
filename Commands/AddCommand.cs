using Beadando_VM_KH.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    internal class AddCommand(CentralStation centralStation) : ICommand
    {
        private readonly CentralStation centralStation = centralStation;

        public string Name => "Add";

        public string Description => "Adds a new sensor of the specified type.";

        public void Execute(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: add <SensorType>");
            }
            else if (Enum.TryParse(args[0], true, out SensorType sensorType))
            {
                Sensor sensor = new(centralStation, sensorType);
                Console.WriteLine($"Added {sensorType} sensor.");
            }
            else
            {
                Console.WriteLine($"Invalid SensorType. Valid types are: {string.Join(", ", Enum.GetValues<SensorType>())}");
            }
        }
    }
}
