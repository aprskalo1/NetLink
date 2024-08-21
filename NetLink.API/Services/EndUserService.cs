using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Shared.DTOs;

namespace NetLink.API.Services;

public interface IEndUserService
{
    Task<string> AddEndUserAsync(EndUserDto endUserDto, string devToken);
    Task EnsureEndUserStatusAsync(string endUserId);
}

public class EndUserService(IMapper mapper, NetLinkDbContext dbContext, IDeveloperService developerService)
    : IEndUserService
{
    public async Task<string> AddEndUserAsync(EndUserDto endUserDto, string devToken)
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
    public async Task EnsureEndUserStatusAsync(string endUserId)
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

    private async Task<bool> ValidateEndUserAssociationAsync(string endUserId, Guid developerId) =>
        await dbContext.DeveloperUsers
            .AnyAsync(du => du.EndUserId == endUserId && du.DeveloperId == developerId);
}