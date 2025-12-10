using Beadando_VM_KH.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{

    /// <summary>
    /// Represents a command that exports current sensor data to a JSON file.
    /// </summary>
    /// <remarks>
    /// The command can optionally filter sensors by type using the <c>--type=&lt;SensorType&gt;</c> argument.
    /// The output filename must be enclosed in quotes. If the filename is missing, empty, or invalid,
    /// an error message is displayed.
    ///
    /// <para>Example usage:</para>
    /// <code>
    /// export "sensors.json"
    /// Exported sensor data to sensors.json
    ///
    /// export --type=Light "lights.json"
    /// Exported sensor data to lights.json
    ///
    /// export
    /// Usage: export [--type=&lt;SensorType&gt;] "&lt;filename&gt;"
    /// </code>
    /// </remarks>
    /// <param name="centralStation">The central station instance containing the sensors to export.</param>
    ///
    /// <author>malackapite</author>
    internal partial class ExportCommand(CentralStation centralStation) : ICommand
    {

        private readonly CentralStation centralStation = centralStation;

        /// <summary>
        /// Gets the name of the command as used in the command-line.
        /// </summary>
        public string Name => "Export";

        /// <summary>
        /// Gets a human-readable description of what the command does.
        /// </summary>
        public string Description => "Exports the current sensor data to a file.";

        /// <summary>
        /// Executes the command by exporting sensor data to the specified JSON file.
        /// </summary>
        /// <param name="args">
        /// Command-line arguments:
        /// <list type="bullet">
        /// <item>Optional: <c>--type=&lt;SensorType&gt;</c> to filter sensors by type.</item>
        /// <item>Required: "<c>filename</c>" enclosed in quotes.</item>
        /// </list>
        /// </param>
        public void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: export [--type=<SensorType>] \"<filename>\"");
            }
            else
            {
                Match match = QuotedStringRegex().Match(string.Join(" ", args));
                IReadOnlyList<Sensor> sensors = centralStation.Sensors;
                if (!match.Success || string.IsNullOrWhiteSpace(match.Groups[1].Value))
                {
                    Console.WriteLine("Filename must be enclosed in quotes and cannot be empty.");
                    return;
                }
                if (args.Length == 2 && args[0].StartsWith("--type="))
                {
                    string typeStr = args[0][7..];
                    if (Enum.TryParse(typeStr, true, out SensorType filterType))
                    {
                        sensors = [.. centralStation.Sensors.Where(s => s.Type == filterType)];
                    }
                    else
                    {
                        Console.WriteLine($"Invalid SensorType. Valid types are: {string.Join(", ", Enum.GetValues<SensorType>())}");
                        return;
                    }
                }
                using StreamWriter writer = new(match.Groups[1].Value);
                writer.Write(JsonConvert.SerializeObject(sensors));
                Console.WriteLine($"Exported sensor data to {match.Groups[1].Value}");
            }
        }
        [GeneratedRegex("\"([^\"]*)\"")]
        private partial Regex QuotedStringRegex();
    }
}
