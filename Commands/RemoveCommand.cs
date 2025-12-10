using Beadando_VM_KH.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    /// <summary>
    /// Represents a command that removes a sensor from the central station.
    /// </summary>
    /// <remarks>
    /// The command expects a single argument: the Id of the sensor to remove.
    /// If no Id is provided, or the given Id does not match any registered sensor, an appropriate error message is displayed.
    /// 
    /// <para>Example usage:</para>
    /// <code>
    /// remove 3f1cba8a
    /// Removed sensor with Id 3f1cba8a-c97a-48ae-abc4-48d692682eff.
    /// </code>
    /// </remarks>
    /// <param name="centralStation">The central station instance that manages registered sensors.</param>
    /// 
    /// <author>malackapite</author>
    internal class RemoveCommand(CentralStation centralStation) : ICommand
    {
        private readonly CentralStation centralStation = centralStation;

        /// <summary>
        /// Gets the name of the command as used in the command-line.
        /// </summary>
        public string Name => "Remove";

        /// <summary>
        /// Gets a short readable description of what the command does.
        /// </summary>
        public string Description => "Removes the sensor with the specified Id.";

        /// <summary>
        /// Executes the command by attempting to remove the sensor with the given Id.
        /// </summary>
        /// <param name="args">
        /// The command-line arguments. The first argument must be the sensor's Id.
        /// </param>
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
