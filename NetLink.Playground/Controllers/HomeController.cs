using Microsoft.AspNetCore.Mvc;
using NetLink.Playground.Models;
using System.Diagnostics;
using NetLink.Models;
using NetLink.Session;
using NetLink.Services;


namespace NetLink.Playground.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEndUserSessionManager _endUserSessionManager;
        private readonly ISensorService _sensorService;

        public HomeController(ILogger<HomeController> logger, IEndUserSessionManager endUserSessionManager, ISensorService sensorService)
        {
            _logger = logger;
            _endUserSessionManager = endUserSessionManager;
            _sensorService = sensorService;
        }

        public IActionResult Index()
        {
            EndUser user1 = new EndUser("4567-e89b-12d3-a456-426614174003");
            _endUserSessionManager.LogEndUserIn(user1);

            return View();
        }

        public IActionResult Privacy()
        {
            Sensor sensor1 = new Sensor("sleepingroom4_sensor");
            _sensorService.AddSensor(sensor1);

            var returnedSensor = _sensorService.GetSensorByName("sleepingroom_sensor");
            Console.WriteLine(returnedSensor.DeviceName);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
