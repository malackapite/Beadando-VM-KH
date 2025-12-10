using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleLocalDB;
using System.ComponentModel.DataAnnotations;

namespace Beadando_VM_KH.model
{
    /// <summary>
    /// Represents the central station component for managing simulated sensors and persisting their generated measurement data.
    /// </summary>
    /// <remarks> 
    /// It is responsible for:
    /// <list type="bullet">
    ///   <item>Registering and unregistering simulated <see cref="Sensor"/> instances.</item>
    ///   <item>Subscribing to simulated value-change events.</item>
    ///   <item>Persisting the generated readings into the simulation database.</item>
    /// </list>
    /// </remarks>
    /// <param name="config">
    /// Application configuration used when creating the database context.
    /// </param>
    /// 
    /// <author>malackapite</author>
    public class CentralStation(IConfiguration config)
    {
        /// <summary>
        /// Application configuration used when creating the database context.
        /// </summary>
        private readonly IConfiguration _config = config;

        /// <summary>
        /// Stores all currently registered sensors.
        /// </summary>
        private readonly List<Sensor> _Sensors = [];
        
        /// <summary>
        /// Gets a read-only collection of all registered sensors.
        /// </summary>
        public IReadOnlyList<Sensor> Sensors => _Sensors.AsReadOnly();
        
        /// <summary>
        /// Provides indexed access to the registered sensors.
        /// </summary>
        /// <param name="ix">Zero-based index of the sensor.</param>
        public Sensor this[int ix]
        {
            get => _Sensors[ix];
        }

        /// <summary>
        /// Registers a sensor and begins listening for its generated values.
        /// </summary>
        /// <param name="s">The sensor to register.</param>
        public void RegisterSensor(Sensor s)
        {
            _Sensors.Add(s);
            s.OnValueChanged += SensorValueReceived;
        }

        /// <summary>
        /// Removes a sensor using prefix matching on its identifier.
        /// </summary>
        /// <param name="id">ID prefix used to locate the sensor.</param>
        /// <returns>
        /// The removed sensor instance, or <c>null</c> if no match was found.
        /// </returns>
        /// <remarks>
        /// The sensor is removed from the simulation, unsubscribed from events, and disposed.  
        /// </remarks>
        public Sensor? RemoveSensor(string id)
        {
            Sensor? sensor = _Sensors.FirstOrDefault(x => x.Id.ToString().StartsWith(id));
            if (sensor != null)
                _Sensors.Remove(sensor);
            sensor?.Dispose();
            return sensor;
        }

        /// <summary>
        /// Handles incoming sensor value changes by persisting them to the database.
        /// </summary>
        /// <param name="sender">The <see cref="Sensor"/> that produced the value.</param>
        /// <param name="value">The generated measurement.</param>
        /// <remarks>
        /// Readings are written synchronously to the database using UTC timestamps.
        /// </remarks>
        private void SensorValueReceived(object sender, double value)
        {
            using AppDbContext<SensorRecord> context = new(config);
            context.values.Add(new SensorRecord
            (
                ((Sensor)sender).Id,
                value,
                DateTime.Now
            ));
            context.SaveChanges();
        }
    }
}
