using Microsoft.AspNetCore.Mvc;
using NetLink.Playground.Models;
using System.Diagnostics;
using NetLink.Models;
using NetLink.Session;
using NetLink.Services;
using NetLink.Statistics;

namespace NetLink.Playground.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    IEndUserSessionManager endUserSessionManager,
    ISensorService sensorService,
    IEndUserManagementService endUserManagementService,
    IRecordedValueService recordedValueService,
    IStatisticsService statistics)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var endUser = new EndUser("48a187e5-3a77-4842-949a-49a85ac0a0e9");
        await endUserSessionManager.LogInEndUserAsync(endUser);

        var sensors = await endUserManagementService.ListEndUserSensorsAsync(endUser.Id!);

        for (var i = 0; i < 10; i++)
        {
            await recordedValueService.RecordValueBySensorNameAsync(new RecordedValue(25), sensors[0].DeviceName, endUser.Id!);
            await recordedValueService.RecordValueBySensorIdAsync(new RecordedValue(21), sensors[1].Id);
        }

        var recordedValues = await recordedValueService.GetRecordedValuesAsync(sensors[0].Id, true, 10);
        var recordedValues2 =
            await recordedValueService.GetRecordedValuesAsync(sensors[0].Id, false, null, DateTime.Now.AddDays(-1), DateTime.Now);

        var value = statistics.GetAverageValue(recordedValues);
        var value1 = statistics.GetMedianValue(recordedValues);
        var value2 = statistics.GetVariance(recordedValues);
        var value3 = statistics.GetStandardDeviation(recordedValues);
        var value4 = statistics.GetMinValue(recordedValues);
        var value5 = statistics.GetMaxValue(recordedValues);


        // var endUser = new EndUser("9d7f978f-ca3b-4c66-9dec-69e88352a64l");
        // await endUserSessionManager.LogInEndUserAsync(endUser);
        //
        // var sensor = new Sensor(
        //     deviceName: "Playground Sensor 3",
        //     deviceType: "Thermometer",
        //     measurementUnit: "Celsius",
        //     deviceLocation: "Room 101",
        //     deviceDescription: "Measures temperature"
        // );
        //
        // var addedSensorId = await sensorService.AddSensorAsync(sensor);
        //
        // var retrievedSensor = await sensorService.GetSensorByIdAsync(addedSensorId);
        //
        // var retrievedSensor2 = await sensorService.GetSensorByNameAsync(sensor.DeviceName!);
        //
        // var sensor2 = new Sensor(
        //     deviceName: "Playground Sensor 3 Updated",
        //     deviceType: "Thermometer",
        //     measurementUnit: "Celsius",
        //     deviceLocation: "Room 101",
        //     deviceDescription: "Measures temperature"
        // );
        //
        // var retrievedSensor3 = await sensorService.UpdateSensorAsync(addedSensorId, sensor2);
        //
        // await sensorService.DeleteSensorAsync(addedSensorId);

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
        // var sensorIds = new List<Guid>
        // {
        //     Guid.Parse("CB3B009A-83A9-4045-AB5E-08DCC6C18E0F"),
        //     Guid.Parse("0542BEB4-92C6-4B61-AB5F-08DCC6C18E0F")
        // };
        //
        // await endUserSessionManager.LogInEndUserAsync(endUser);
        //
        // await endUserManagementService.AssignSensorsToEndUserAsync(sensorIds, endUserSessionManager.GetLoggedEndUserId());

        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        // var user = new EndUser("77a9fcf4-da59-4619-85a6-b7fdbad6944a");
        // await endUserManagementService.ValidateEndUserAsync(user.Id!);
        //
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