using AutoMapper;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Repositories;

namespace NetLink.API.Services;

public interface IEndUserService
{
    Task<string> RegisterEndUserAsync(EndUserRequestDto endUserRequestDto, string devToken);
    Task<EndUserResponseDto> GetEndUserByIdAsync(string endUserId, string devToken);
    Task ValidateEndUserAsync(string endUserId);
    Task<List<EndUserResponseDto>> ListDevelopersEndUsersAsync(string devToken); //TODO: Put this in developer service
    Task<PagedEndUserResponseDto> ListPagedDevelopersEndUsersAsync(Guid developerId, int page, int pageSize); //TODO: Put this in developer service
    Task DeactivateEndUserAsync(string endUserId);
    Task ReactivateEndUserAsync(string endUserId);
    Task SoftDeleteEndUserAsync(string endUserId);
    Task RestoreEndUserAsync(string endUserId);
    Task AssignSensorsToEndUserAsync(List<Guid> sensorIds, string endUserId);
    Task<List<SensorResponseDto>> ListEndUserSensorsAsync(string endUserId); //TODO: Put this in sensor service

    //TODO: Add methods to assign and remove groups from end users
}

public class EndUserService(IMapper mapper, IDeveloperService developerService, IEndUserRepository endUserRepository)
    : IEndUserService
{
    public async Task<string> RegisterEndUserAsync(EndUserRequestDto endUserRequestDto, string devToken)
    {
        var developerId = await developerService.GetDeveloperIdFromTokenAsync(devToken);
        var endUser = mapper.Map<EndUser>(endUserRequestDto);

        if (await endUserRepository.ValidateEndUserAssociationAsync(endUserRequestDto.Id!, developerId))
            throw new EndUserException($"EndUser with ID: {endUserRequestDto.Id} already exists, please use another account.");

        await endUserRepository.AddEndUserAsync(endUser);

        var developerUser = new DeveloperUser
        {
            DeveloperId = developerId,
            EndUserId = endUser.Id
        };

        await endUserRepository.AddDeveloperUserAsync(developerUser);
        await endUserRepository.SaveChangesAsync();

        return endUser.Id!;
    }

    public async Task<EndUserResponseDto> GetEndUserByIdAsync(string endUserId, string devToken)
    {
        var developerId = await developerService.GetDeveloperIdFromTokenAsync(devToken);
        if (!await endUserRepository.ValidateEndUserAssociationAsync(endUserId, developerId))
            throw new EndUserException($"EndUser with ID {endUserId} does not exist or is not associated with the developer.");

        var endUser = await endUserRepository.GetEndUserByIdAsync(endUserId);
        return mapper.Map<EndUserResponseDto>(endUser);
    }

    public async Task ValidateEndUserAsync(string endUserId)
    {
        var endUser = await endUserRepository.GetEndUserByIdAsync(endUserId);

        if (endUser.DeletedAt.HasValue)
            throw new EndUserException($"EndUser's account with ID {endUserId} has been deleted at {endUser.DeletedAt}.");

        if (!endUser.Active)
            throw new EndUserException($"EndUser's account with ID {endUserId} is not active.");
    }

    public async Task<List<EndUserResponseDto>> ListDevelopersEndUsersAsync(string devToken)
    {
        await developerService.ValidateDeveloperAsync(devToken);
        var developerId = await developerService.GetDeveloperIdFromTokenAsync(devToken);
        var endUsers = await endUserRepository.ListDeveloperEndUsersAsync(developerId);
        return mapper.Map<List<EndUserResponseDto>>(endUsers);
    }

    public async Task<PagedEndUserResponseDto> ListPagedDevelopersEndUsersAsync(Guid developerId, int page, int pageSize)
    {
        var (endUsers, totalCount) = await endUserRepository.ListPagedDeveloperEndUsersAsync(developerId, page, pageSize);

        if (endUsers == null || endUsers.Count == 0)
        {
            throw new NotFoundException("No end users found for the given developer.");
        }

        var endUserDtos = mapper.Map<List<EndUserResponseDto>>(endUsers);

        return new PagedEndUserResponseDto
        {
            EndUsers = endUserDtos,
            TotalCount = totalCount
        };
    }


    public async Task DeactivateEndUserAsync(string endUserId)
    {
        var endUser = await endUserRepository.GetEndUserByIdAsync(endUserId);
        endUser.Active = false;
        await endUserRepository.SaveChangesAsync();
    }

    public async Task ReactivateEndUserAsync(string endUserId)
    {
        var endUser = await endUserRepository.GetEndUserByIdAsync(endUserId);
        endUser.Active = true;
        await endUserRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteEndUserAsync(string endUserId)
    {
        var endUser = await endUserRepository.GetEndUserByIdAsync(endUserId);
        endUser.DeletedAt = DateTime.Now;
        await endUserRepository.SaveChangesAsync();
    }

    public async Task RestoreEndUserAsync(string endUserId)
    {
        var endUser = await endUserRepository.GetEndUserByIdAsync(endUserId);
        endUser.DeletedAt = null;
        await endUserRepository.SaveChangesAsync();
    }

    public async Task AssignSensorsToEndUserAsync(List<Guid> sensorIds, string endUserId)
    {
        await ValidateEndUserAsync(endUserId);

        var endUserSensors = new List<EndUserSensor>();
        var endUser = await endUserRepository.GetEndUserByIdAsync(endUserId);
        var sensors = await endUserRepository.GetSensorsByIdsAsync(sensorIds);

        if (sensorIds.Count == 0)
            throw new EndUserException($"No sensors provided to assign to EndUser with ID {endUserId}.");

        if (sensorIds.Count != sensors.Count)
            throw new NotFoundException("One or more sensors do not exist.");

        foreach (var sensor in sensors)
        {
            if (await endUserRepository.IsEndUserSensorAssignedAsync(sensor.Id, endUser.Id!))
                continue;

            endUserSensors.Add(new EndUserSensor
            {
                SensorId = sensor.Id,
                EndUserId = endUser.Id
            });
        }

        if (endUserSensors.Count != 0)
        {
            await endUserRepository.AddEndUserSensorsAsync(endUserSensors);
            await endUserRepository.SaveChangesAsync();
        }
        else
        {
            throw new EndUserException($"All sensors are already assigned to EndUser with ID {endUserId}.");
        }
    }

    public async Task<List<SensorResponseDto>> ListEndUserSensorsAsync(string endUserId)
    {
        await ValidateEndUserAsync(endUserId);

        var endUserSensors = await endUserRepository.ListEndUserSensorsAsync(endUserId);
        return mapper.Map<List<SensorResponseDto>>(endUserSensors);
    }
}