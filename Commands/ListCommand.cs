using Beadando_VM_KH.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    /// <summary>
    /// Represents a command that lists all registered sensors in the central station.
    /// </summary>
    /// <remarks>
    /// The output consists of one line per sensor. Each line contains:
    /// <list type="bullet">
    /// <item><c>Id</c>: the sensor's unique identifier (GUID)</item>
    /// <item><c>Type</c>: the sensor's category (e.g., Light, Temperature)</item>
    /// <item><c>Value</c>: the sensor's current measured value.</item>
    /// </list>
    /// Example output:
    /// <code>
    /// Id: 3f1cba8a..., Type: Light, Value: 195.39
    /// Id: e4bef1a8..., Type: Light, Value: 986.66
    /// </code>
    /// </remarks>
    /// 
    /// <author>malackapite</author>
    internal class ListCommand(CentralStation centralStation) : ICommand
    {
        private readonly CentralStation centralStation = centralStation;

        /// <summary>
        /// Gets the name of the command as used by the command-line interface.
        /// </summary>
        public string Name => "List";
        
        /// <summary>
        /// Gets a short readable description of the command.
        /// </summary>
        public string Description => "Lists all registered sensors.";

        /// <summary>
        /// Executes the command and prints all sensors ordered by their ID.
        /// </summary>
        /// <param name="args">
        /// Optional command arguments (ignored by this command).
        /// </param>
        public void Execute(string[] args)
        {
            foreach (var sensor in centralStation.Sensors.OrderBy(x => x.Id))
            {
                Console.WriteLine(sensor);
            }
        }
    }
}
