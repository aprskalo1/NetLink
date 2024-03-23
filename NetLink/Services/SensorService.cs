using NetLink.Models;
using NetLink.Session;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLink.Services
{
    public interface ISensorService
    {
        public Sensor AddSensor(Sensor sensor);
        public Sensor GetSensorByName(string deviceName);
    }

    internal class SensorService : ISensorService
    {
        private readonly IEndUserSessionManager _endUserSessionManager;

        public SensorService(IEndUserSessionManager endUserSessionManager)
        {
            _endUserSessionManager = endUserSessionManager;
        }

        public Sensor AddSensor(Sensor sensor)
        {
            string endUserId = _endUserSessionManager.GetLoggedEndUserId();
            string addSensorEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.AddSensorUrl, endUserId)}";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(sensor);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = httpClient.PostAsync(addSensorEndpoint, content).Result;
                    response.EnsureSuccessStatusCode();

                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    //Sensor apiSensor = JsonConvert.DeserializeObject<Sensor>(responseBody)!;

                    return sensor;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("An error occurred while adding the sensor. Please check your network connection and try again.", ex);
                }
            }
        }

        public Sensor GetSensorByName(string deviceName)
        {
            string endUserId = _endUserSessionManager.GetLoggedEndUserId();
            string getSensorByNameEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetSensorByNameUrl, deviceName, endUserId)}";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(getSensorByNameEndpoint).Result;
                    response.EnsureSuccessStatusCode();

                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    Sensor sensor = JsonConvert.DeserializeObject<Sensor>(responseBody)!;

                    return sensor;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("An error occurred while getting the sensor. Please check your network connection and try again.", ex);
                }
            }

        }
    }
}
