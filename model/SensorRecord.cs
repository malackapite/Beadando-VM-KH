using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.model
{
    /// <summary>
    /// Represents a single sensor record  stored in the database.
    /// </summary>
    /// <remarks>
    /// This class is an Entity Framework Core entity and corresponds to a table that stores sensor measurements.  
    /// Each record is uniquely identified by the combination of <see cref="SensorId"/> and <see cref="Timestamp"/>.
    /// </remarks>
    /// <param name="sensorId">The unique identifier of the sensor that generated the record.</param>
    /// <param name="value">The numeric value recorded from the sensor.</param>
    /// <param name="timestamp">The time when the measurement was taken.</param
    /// 
    /// <author>malackapite</author>
    [PrimaryKey(nameof(SensorId), nameof(Timestamp))]
    internal class SensorRecord(Guid sensorId, double value, DateTime timestamp)
    {
        public Guid SensorId { get; set; } = sensorId;
        public double Value { get; set; } = value;
        public DateTime Timestamp { get; set; } = timestamp;
    }
}
