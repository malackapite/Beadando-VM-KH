using Beadando_VM_KH.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.Commands
{
    internal class ListCommand(CentralStation centralStation) : ICommand
    {
        private readonly CentralStation centralStation = centralStation;
        public string Name => "List";
        public string Description => "Lists all registered sensors.";
        public void Execute(string[] args)
        {
            foreach (var sensor in centralStation.Sensors)
            {
                Console.WriteLine(sensor);
            }
        }
    }
}
