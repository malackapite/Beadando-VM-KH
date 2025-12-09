using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.model
{
    /// <summary>
    /// Represents a record of sensor data, including the sensor identifier, the recorded value, and the timestamp of the reading.
    /// </summary>
    /// This class is primarily used to store and manage individual sensor readings. 
    /// Each record is uniquely identified by a combination of the sensor ID and the timestamp.
    /// <param name="sensorId"></param>
    /// <param name="value"></param>
    /// <param name="timestamp"></param>
    /// <author>malackapite</author>
    [PrimaryKey(nameof(SensorId), nameof(Timestamp))]
    internal class SensorRecord(Guid sensorId, double value, DateTime timestamp)
    {
        public Guid SensorId { get; set; } = sensorId;
        public double Value { get; set; } = value;
        public DateTime Timestamp { get; set; } = timestamp;
    }
}
