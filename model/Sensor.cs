using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Beadando_VM_KH.model
{
    /// <summary>
    /// Represents a simulated sensor that periodically generates random values based on its <see cref="SensorType"/>.
    /// </summary>
    /// <remarks>
    /// Each sensor emits values at random intervals and sends the generated readings through the <see cref="OnValueChanged"/> event.
    /// The values are artificial and intended solely for simulation/testing.
    /// </remarks>
    /// <param name="centralStation">
    /// The central station that will register and manage this simulated sensor.
    /// </param>
    /// <param name="type">
    /// The simulated sensor's measurement type, determining its value range.
    /// </param>
    /// 
    /// <author>malackapite</author>
    public class Sensor : IDisposable
    {
        /// <summary>
        /// Random number generator used for simulated measurement generation.
        /// </summary>
        private readonly Random Rnd;

        /// <summary>
        /// Background timer used to trigger value updates at random intervals.
        /// </summary>
        private readonly Timer Timer;

        /// <summary>
        /// Gets the unique identifier of the simulated sensor.
        /// </summary>
        [XmlAttribute("id")]
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the measurement type that defines the sensor's simulated value range.
        /// </summary>
        [XmlAttribute("type")]
        public SensorType Type { get; init; }

        /// <summary>
        /// Gets the generated simulated values.
        /// </summary>
        public List<double> Values { get; private set; } = [];

        /// <summary>
        /// Occurs whenever the sensor generates a new simulated value.
        /// </summary>
        public event Action<object, double>? OnValueChanged;

        /// <summary>
        /// Initializes a new simulated sensor and registers it with the central station.
        /// </summary>
        /// <remarks>
        /// The sensor immediately begins generating values at random intervals
        /// between 1 and 10 seconds.
        /// </remarks>
        public Sensor(CentralStation centralStation, SensorType type)
        {
            Rnd = new();
            Id = Guid.NewGuid();
            Type = type;
            Timer = new(_ => SetValue(), null, 0, Rnd.Next(1000, 10001));
            centralStation.RegisterSensor(this);
        }

        public Sensor() : this(null!, SensorType.Temperature) { }

        /// <summary>
        /// Generates a new simulated value based on the sensor type and raises the event.
        /// </summary>
        void SetValue()
        {
            Values.Add(Type switch
            {
                SensorType.Temperature => Rnd.Next(-20, 40) + Rnd.NextDouble(),
                SensorType.Humidity => Rnd.Next(0, 100) + Rnd.NextDouble(),
                SensorType.Pressure => Rnd.Next(950, 1050) + Rnd.NextDouble(),
                SensorType.Light => Rnd.Next(0, 1000) + Rnd.NextDouble(),
                SensorType.Motion => Rnd.Next(0, 2),
                _ => throw new ArgumentOutOfRangeException()
            });
            OnValueChanged?.Invoke(this, Values.Last());
        }

        /// <summary>
        /// Returns a string representation of the sensor state.
        /// </summary>
        public override string? ToString()
        {
            return $"Id: {Id}, Type: {Type}, Values: {string.Join(", ", Values.Select(x => $"{x:f2}"))}";
        }

        /// <summary>
        /// Stops value generation and releases resources.
        /// </summary>
        public void Dispose()
        {
            Timer.Dispose();
            OnValueChanged = null;
        }
    }

    /// <summary>
    /// Defines the available sensor types.
    /// </summary>
    public enum SensorType
    {
        Temperature,
        Humidity,
        Pressure,
        Light,
        Motion
    }
}

