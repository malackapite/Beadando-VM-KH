using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando_VM_KH.model
{
    [PrimaryKey(nameof(SensorId), nameof(Timestamp))]
    internal class SensorRecord(Guid sensorId, double value, DateTime timestamp)
    {
        public Guid SensorId { get; set; } = sensorId;
        public double Value { get; set; } = value;
        public DateTime Timestamp { get; set; } = timestamp;
    }
}
