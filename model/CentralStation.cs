using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.model
{
    internal class CentralStation
    {
        public List<Sensor> Sensors { get; private set; } = [];
        public Sensor this[int ix]
        {
            get => Sensors[ix];
        }
        public void RegisterSensor(Sensor s)
        {
            Sensors.Add(s);
            s.OnValueChanged += SensorValueReceived;
        }

        private void SensorValueReceived(object sender, double value)
        {
            throw new NotImplementedException();
        }
    }
}
