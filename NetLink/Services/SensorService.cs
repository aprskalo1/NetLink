using NetLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLink.Services
{
    public interface ISensorService
    {
        public void AddSensor(Sensor sensor);
    }

    internal class SensorService : ISensorService
    {
        public void AddSensor(Sensor sensor)
        {
            throw new NotImplementedException();
        }
    }
}
