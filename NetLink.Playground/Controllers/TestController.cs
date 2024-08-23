using Microsoft.AspNetCore.Mvc;
using NetLink.Models;
using NetLink.Services;

namespace NetLink.Playground.Controllers;

public class TestController : Controller
{
    private readonly ISensorService _sensorService;

    public TestController(ISensorService sensorService)
    {
        _sensorService = sensorService;
    }

    // GET
    public IActionResult Privacy2()
    {
        // Sensor sensor1 = new Sensor("playground_test_sensor");
        // _sensorService.AddSensorAsync(sensor1);

        //sensor1.RecordValue("nova vrijednostttt", _recordedValueService);

        //var returnedSensor = _sensorService.GetSensorByName("sleepingroom_sensor");
        //Console.WriteLine(returnedSensor.DeviceName);
        
        Sensor sensor1 = new Sensor("playground_test_sensor");
        _sensorService.AddSensorAsync(sensor1);

        return View();
    }
}