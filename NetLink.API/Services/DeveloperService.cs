using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Shared.DTOs;

namespace NetLink.API.Services;

public interface IDeveloperService
{
    Task<Guid> AddTokenAsync(DeveloperDto developerDto);
    Task CheckIfDevTokenExistsAsync(string devToken);
    Task<Guid> GetDeveloperIdFromTokenAsync(string devToken);
}

public class DeveloperService(IMapper mapper, NetLinkDbContext dbContext) : IDeveloperService
{
    public async Task<Guid> AddTokenAsync(DeveloperDto developerDto)
    {
        await CheckIfDeveloperExistsAsync(developerDto.Username!);

        var developer = mapper.Map<Developer>(developerDto);
        dbContext.Developers.Add(developer);
        await dbContext.SaveChangesAsync();

        return developer.Id;
    }

    public async Task CheckIfDevTokenExistsAsync(string devToken)
    {
        if (!await dbContext.Developers.AnyAsync(d => d.DevToken == devToken && d.Active))
            throw new DeveloperException("DevToken does not exist or developer account is not active.");
    }

    public async Task<Guid> GetDeveloperIdFromTokenAsync(string devToken)
    {
        var developer = await dbContext.Developers.FirstOrDefaultAsync(d => d.DevToken == devToken);

        if (developer == null)
            throw new DeveloperException(
                "Developer with this token does not exist or DevToken is invalid, please check your DevToken in configuration file.");

        return developer.Id;
    }

    private async Task CheckIfDeveloperExistsAsync(string username)
    {
        if (await dbContext.Developers.FirstOrDefaultAsync(d => d.Username == username) != null)
            throw new DeveloperException("Developer with this username already exists, please use another account.");
    }
}