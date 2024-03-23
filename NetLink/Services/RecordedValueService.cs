using NetLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NetLink.Session;

namespace NetLink.Services
{
    public interface IRecordedValueService
    {
        public RecordedValue RecordValue(string deviceName, RecordedValue recordedValue);
    }

    internal class RecordedValueService : IRecordedValueService
    {
        private readonly IEndUserSessionManager _endUserSessionManager;

        public RecordedValueService(IEndUserSessionManager endUserSessionManager)
        {
            _endUserSessionManager = endUserSessionManager;
        }

        public RecordedValue RecordValue(string deviceName, RecordedValue recordedValue)
        {
            var endUserId = _endUserSessionManager.GetLoggedEndUserId();
            string recordValueEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.AddRecordedValueUrl, deviceName, endUserId)}";

            using (var http = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(recordedValue);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = http.PostAsync(recordValueEndpoint, content).Result;
                    response.EnsureSuccessStatusCode();

                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    return recordedValue;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception("An error occurred while recording the value. Please check your network connection and try again.", ex);
                }
            }
        }
    }
}
