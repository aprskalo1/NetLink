using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLink.Models
{
    public class Sensor
    {
        public Sensor(string? deviceName)
        {
            DeviceName = deviceName;
        }

        public string? DeviceName { get; set; }
    }
}
