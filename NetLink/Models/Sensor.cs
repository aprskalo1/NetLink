using Microsoft.VisualBasic;
using NetLink.Services;
using NetLink.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLink.Models
{
    public class Sensor
    {
        public string? DeviceName { get; set; }

        public Sensor(string? deviceName)
        {
            DeviceName = deviceName;
        }

        public RecordedValue RecordValue(string value, IRecordedValueService recordedValueService)
        {
            if (string.IsNullOrEmpty(DeviceName))
            {
                throw new Exception("Device name is required.");
            }

            var recordedValue = new RecordedValue(value);
            return recordedValueService.RecordValue(DeviceName!, recordedValue);
        }
    }
}
