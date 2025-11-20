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
    

    internal partial class ExportCommand(CentralStation centralStation) : ICommand
    {

        private readonly CentralStation centralStation = centralStation;
        public string Name => "Export";
        public string Description => "Exports the current sensor data to a file.";
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
