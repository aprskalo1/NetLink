using Microsoft.AspNetCore.Mvc;
using NetLink.Models;
using NetLink.Services;
using NetLink.Session;

namespace NetLink.Playground.Controllers;

public class TestController(
    ISensorService sensorService,
    IEndUserManagementService endUserManagementService,
    IGroupingService groupingService,
    IEndUserSessionManager endUserSessionManager) : Controller
{
    public async Task<IActionResult> Privacy2()
    {
        var endUser1 = new EndUser("8da6e136-33ab-4b03-869d-f5f3a503e16k");
        var endUser2 = new EndUser("8da6e136-33ab-4b03-869d-f5f3a503e16f");
        await endUserManagementService.RegisterEndUserAsync(endUser1);
        await endUserSessionManager.LogInEndUserAsync(endUser1);

        var sensor = new Sensor(
            deviceName: "Playground Sensor 6",
            deviceType: "Thermometer",
            measurementUnit: "Celsius",
            deviceLocation: "Room 101",
            deviceDescription: "Measures temperature"
        );
        await sensorService.AddSensorAsync(sensor);
        await sensorService.AddSensorAsync(sensor, endUser2.Id);

        var endUsers1Sensors = await endUserManagementService.ListEndUserSensorsAsync(endUser1.Id!);

        //GROUPING TEST

        var group1 = new Group(
            groupName: "Group 1"
        );

        var insertedGroupId = await groupingService.CreateGroupAsync(group1);

        var insertedGroup2Id = await groupingService.CreateGroupAsync(group1, endUser2.Id);
        await groupingService.DeleteGroupAsync(insertedGroup2Id, endUser2.Id);

        foreach (var endUsers1Sensor in endUsers1Sensors)
        {
            await groupingService.AddSensorToGroupAsync(insertedGroupId, endUsers1Sensor.Id);
        }

        await groupingService.RemoveSensorFromGroupAsync(insertedGroupId, endUsers1Sensors[0].Id);

        var endUser1Groups = await groupingService.GetEndUserGroupsAsync();
        var endUser2Groups = await groupingService.GetEndUserGroupsAsync(endUser2.Id);

        await groupingService.GetGroupByIdAsync(insertedGroupId);

        var updatedGroup1 = new Group("Updated Group 1");
        await groupingService.UpdateGroupAsync(updatedGroup1, insertedGroupId);

        await groupingService.DeleteGroupAsync(insertedGroupId);

        return View();
    }
}