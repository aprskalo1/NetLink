using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetLink.Services
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddSensorServices(this IServiceCollection services)
        {
            services.AddSingleton<ISensorService, SensorService>();
            services.AddSingleton<IRecordedValueService, RecordedValueService>();

            return services;
        }
    }
}
