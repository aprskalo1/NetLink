using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Shared;
using NetLink.API.Shared.DTOs;

namespace NetLink.API.Services;

public interface IEndUserService
{
    Task<string> RegisterEndUserAsync(EndUserDto endUserDto, string devToken);
    Task<EndUserRes> GetEndUserByIdAsync(string endUserId);
    Task ValidateEndUserAsync(string endUserId);
    Task<List<EndUserRes>> ListDevelopersEndUsersAsync(string devToken);
    Task DeactivateEndUserAsync(string endUserId);
    Task ReactivateEndUserAsync(string endUserId);
    Task SoftDeleteEndUserAsync(string endUserId);
    Task RestoreEndUserAsync(string endUserId);
    Task AssignSensorsToEndUserAsync(List<Guid> sensorIds, string endUserId);
}

public class EndUserService(IMapper mapper, NetLinkDbContext dbContext, IDeveloperService developerService)
    : IEndUserService
{
    public async Task<string> RegisterEndUserAsync(EndUserDto endUserDto, string devToken)
    {
        var developerId = await developerService.GetDeveloperIdFromTokenAsync(devToken);
        var endUser = mapper.Map<EndUser>(endUserDto);

        if (await ValidateEndUserAssociationAsync(endUserDto.Id!, developerId))
            throw new EndUserException("EndUser already exists, please use another account.");

        var developerUser = new DeveloperUser
        {
            DeveloperId = developerId,
            EndUserId = endUser.Id
        };

        dbContext.EndUsers.Add(endUser);
        dbContext.DeveloperUsers.Add(developerUser);
        await dbContext.SaveChangesAsync();

        return endUser.Id!;
    }

    //TODO: Add devToken or developerId for future use for security reasons
    public async Task<EndUserRes> GetEndUserByIdAsync(string endUserId)
    {
        var endUser = await dbContext.EndUsers.FirstOrDefaultAsync(e => e.Id == endUserId);

        if (endUser == null)
            throw new NotFoundException("EndUser does not exist.");

        return mapper.Map<EndUserRes>(endUser);
    }

    //TODO: Add devToken or developerId for future use for security reasons
    public async Task ValidateEndUserAsync(string endUserId)
    {
        var endUser = await dbContext.EndUsers
            .FirstOrDefaultAsync(e => e.Id == endUserId);

        if (endUser == null)
            throw new NotFoundException("EndUser does not exist.");

        if (endUser.DeletedAt.HasValue)
            throw new EndUserException("EndUser account has been deleted.");

        if (!endUser.Active)
            throw new EndUserException("EndUser account is not active.");
    }

    public async Task<List<EndUserRes>> ListDevelopersEndUsersAsync(string devToken)
    {
        await developerService.ValidateDeveloperAsync(devToken);

        var developersUsers = await dbContext.DeveloperUsers
            .Where(du => du.Developer!.DevToken == devToken)
            .Select(du => mapper.Map<EndUserRes>(du.EndUser))
            .ToListAsync();

        return developersUsers;
    }

    public async Task DeactivateEndUserAsync(string endUserId)
    {
        var endUser = await FindEndUserByIdAsync(endUserId);
        endUser.Active = false;
        await dbContext.SaveChangesAsync();
    }

    public async Task ReactivateEndUserAsync(string endUserId)
    {
        var endUser = await FindEndUserByIdAsync(endUserId);
        endUser.Active = true;
        await dbContext.SaveChangesAsync();
    }

    public async Task SoftDeleteEndUserAsync(string endUserId)
    {
        var endUser = await FindEndUserByIdAsync(endUserId);
        endUser.DeletedAt = DateTime.Now;
        await dbContext.SaveChangesAsync();
    }

    public async Task RestoreEndUserAsync(string endUserId)
    {
        var endUser = await FindEndUserByIdAsync(endUserId);
        endUser.DeletedAt = null;
        await dbContext.SaveChangesAsync();
    }

    public async Task AssignSensorsToEndUserAsync(List<Guid> sensorIds, string endUserId)
    {
        await ValidateEndUserAsync(endUserId);

        var endUserSensors = new List<EndUserSensor>();
        var endUser = await FindEndUserByIdAsync(endUserId);
        var sensors = await dbContext.Sensors
            .Where(s => sensorIds.Contains(s.Id))
            .ToListAsync();

        if (sensorIds.Count == 0)
            throw new EndUserException("No sensors to assign.");

        if (endUser == null)
            throw new NotFoundException("EndUser with this Id does not exist.");

        if (sensorIds.Count != sensors.Count)
            throw new NotFoundException("One or more sensors do not exist.");

        foreach (var sensor in sensors)
        {
            if (await dbContext.EndUserSensors.AnyAsync(eus => eus.SensorId == sensor.Id && eus.EndUserId == endUser.Id))
                continue;

            endUserSensors.Add(new EndUserSensor
            {
                SensorId = sensor.Id,
                EndUserId = endUser.Id
            });
        }

        if (endUserSensors.Count != 0)
        {
            dbContext.EndUserSensors.AddRange(endUserSensors);
            await dbContext.SaveChangesAsync();
        }
        else
        {
            throw new EndUserException("All sensors are already assigned to this EndUser.");
        }
    }

    private async Task<EndUser> FindEndUserByIdAsync(string id)
    {
        var endUser = await dbContext.EndUsers.FirstOrDefaultAsync(e => e.Id == id);

        if (endUser == null)
            throw new NotFoundException("EndUser with this Id does not exist.");

        return endUser;
    }

    private async Task<bool> ValidateEndUserAssociationAsync(string endUserId, Guid developerId) =>
        await dbContext.DeveloperUsers
            .AnyAsync(du => du.EndUserId == endUserId && du.DeveloperId == developerId);
}