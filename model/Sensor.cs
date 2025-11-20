using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Beadando_VM_KH.model
{
    internal class Sensor : IDisposable
    {
        private readonly Random Rnd;
        private readonly Timer Timer;

        public readonly Guid Id;
        public readonly SensorType Type;
        public double Value { get; private set; }
        public event Action<object, double>? OnValueChanged;

        public Sensor(CentralStation centralStation, SensorType type)
        {
            Rnd = new();
            Id = Guid.NewGuid();
            Type = type;
            Timer = new(_ => SetValue(), null, 0, Rnd.Next(100, 1001));
            centralStation.RegisterSensor(this);
        }
        void SetValue()
        {
            Value = Type switch
            {
                SensorType.Temperature => Rnd.Next(-20, 40) + Rnd.NextDouble(),
                SensorType.Humidity => Rnd.Next(0, 100) + Rnd.NextDouble(),
                SensorType.Pressure => Rnd.Next(950, 1050) + Rnd.NextDouble(),
                SensorType.Light => Rnd.Next(0, 1000) + Rnd.NextDouble(),
                SensorType.Motion => Rnd.Next(0, 2),
                _ => throw new ArgumentOutOfRangeException()
            };
            OnValueChanged?.Invoke(this, Value);
        }

        public override string? ToString()
        {
            return $"Id: {Id}, Type: {Type}, Value: {Value}";
        }

        public void Dispose()
        {
            Timer.Dispose();
            OnValueChanged = null;
        }
    }
    public enum SensorType
    {
        Temperature,
        Humidity,
        Pressure,
        Light,
        Motion
    }
}

