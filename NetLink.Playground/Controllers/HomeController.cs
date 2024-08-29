using Microsoft.AspNetCore.Mvc;
using NetLink.Playground.Models;
using System.Diagnostics;
using NetLink.Models;
using NetLink.Session;
using NetLink.Services;

namespace NetLink.Playground.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    IEndUserSessionManager endUserSessionManager,
    ISensorService sensorService,
    IRecordedValueService recordedValueService,
    IEndUserManagementService endUserManagementService)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var endUser = new EndUser("9d7f978f-ca3b-4c66-9dec-69e88352a64l");
        // var res = await endUserManagementService.RegisterEndUserAsync(endUser);
        //
        // await endUserSessionManager.LogInEndUserAsync(endUser);
        //
        // await endUserManagementService.ValidateEndUserAsync(endUser.Id!);
        
        // var returnedEndUser = await endUserManagementService.GetEndUserByIdAsync(endUser.Id!);
        
        // await endUserManagementService.ListDevelopersEndUsersAsync();

        // await endUserManagementService.DeactivateEndUserAsync("028d2c0e-08e0-4859-acb7-1dde88d4f095");
        // await endUserManagementService.ReactivateEndUserAsync("028d2c0e-08e0-4859-acb7-1dde88d4f095");
        //
        // await endUserManagementService.SoftDeleteEndUserAsync("028d2c0e-08e0-4859-acb7-1dde88d4f095");
        // await endUserManagementService.RestoreEndUserAsync("028d2c0e-08e0-4859-acb7-1dde88d4f095");
        //
        var sensorIds = new List<Guid>
        {
            Guid.Parse("CB3B009A-83A9-4045-AB5E-08DCC6C18E0F"),
            Guid.Parse("0542BEB4-92C6-4B61-AB5F-08DCC6C18E0F")
        };
        
        await endUserSessionManager.LogInEndUserAsync(endUser);

        await endUserManagementService.AssignSensorsToEndUserAsync(sensorIds, endUserSessionManager.GetLoggedEndUserId());

        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        var user = new EndUser("a3af25fb-ac68-4de7-b2c8-431c1fcae994");

        await endUserSessionManager.LogInEndUserAsync(user);

        var sensor = new Sensor(
            deviceName: "Temperature Sensor 6",
            deviceType: "Thermometer",
            measurementUnit: "Celsius",
            deviceLocation: "Room 101",
            deviceDescription: "Measures temperature"
        );

        var sensorId = await sensorService.AddSensorAsync(sensor);
        Console.WriteLine(sensorId);


        // _endUserSessionManager.GetLoggedEndUserId();
        // _endUserSessionManager.LogOutEndUser();
        // _endUserSessionManager.GetLoggedEndUserId();

        // Sensor sensor1 = new Sensor("playground_test_sensor");
        // _sensorService.AddSensorAsync(sensor1);

        //sensor1.RecordValue("nova vrijednostttt", _recordedValueService);

        //var returnedSensor = _sensorService.GetSensorByName("sleepingroom_sensor");
        //Console.WriteLine(returnedSensor.DeviceName);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}