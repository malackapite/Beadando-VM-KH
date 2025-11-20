namespace Beadando_VM_KH.model
{
    internal class CentralStation
    {
        private readonly List<Sensor> _Sensors = [];
        public IReadOnlyList<Sensor> Sensors => _Sensors.AsReadOnly();
        public Sensor this[int ix]
        {
            get => _Sensors[ix];
        }
        public void RegisterSensor(Sensor s)
        {
            _Sensors.Add(s);
            s.OnValueChanged += SensorValueReceived;
        }

        public Sensor? RemoveSensor(string id)
        {
            Sensor? sensor = _Sensors.FirstOrDefault(x => x.Id.ToString().StartsWith(id));
            if (sensor != null)
                _Sensors.Remove(sensor);
            sensor?.Dispose();
            return sensor;
        }

        private void SensorValueReceived(object sender, double value)
        {
            //throw new NotImplementedException();
        }
    }
}
