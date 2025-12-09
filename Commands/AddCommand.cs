using Beadando_VM_KH.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    /// <summary>
    /// Represents a command that adds a new sensor to the central station.
    /// </summary>
    /// <remarks>
    /// The command expects one argument: the type of sensor to add.
    /// If the provided sensor type is invalid or missing, an error message is displayed.
    ///
    /// <para>Example usage:</para>
    /// <code>
    /// add Light
    /// Added Light sensor.
    /// 
    /// add InvalidType
    /// Invalid SensorType. Valid types are: Light, Temperature, ...
    /// </code>
    /// </remarks>
    /// <param name="centralStation">The central station instance to which the sensor will be added.</param>
    /// <author>malackapite</author>
    internal class AddCommand(CentralStation centralStation) : ICommand
    {
        private readonly CentralStation centralStation = centralStation;

        /// <summary>
        /// Gets the name of the command as used in the command-line.
        /// </summary>
        public string Name => "Add";

        /// <summary>
        /// Gets a readable description of what the command does.
        /// </summary>
        public string Description => "Adds a new sensor of the specified type.";

        /// <summary>
        /// Executes the command by attempting to add a sensor of the specified type.
        /// </summary>
        /// <param name="args">
        /// Command-line arguments. The first argument must be a valid <see cref="SensorType"/>.
        /// </param>
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
