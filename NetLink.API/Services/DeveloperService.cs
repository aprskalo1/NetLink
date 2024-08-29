using System.Security.Cryptography;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Shared;
using NetLink.API.Shared.DTOs;

namespace NetLink.API.Services;

public interface IDeveloperService
{
    Task<Guid> AddDeveloperAsync(DeveloperDto developerDto);
    Task ValidateDeveloperAsync(string devToken);
    Task<Guid> GetDeveloperIdFromTokenAsync(string devToken);
    Task<DeveloperRes> GetDeveloperByIdAsync(Guid id);
    Task<DeveloperRes> GetDeveloperByUsernameAsync(string username);
    Task<DeveloperRes> UpdateDeveloperAsync(Guid id, DeveloperDto developerDto);
    Task DeactivateDeveloperAsync(Guid id);
    Task ReactivateDeveloperAsync(Guid id);
    Task SoftDeleteDeveloperAsync(Guid id);
    Task RestoreDeveloperAsync(Guid id);
    Task DeleteDeveloperAsync(Guid id);
    Task<List<DeveloperRes>> ListDevelopersAsync(); //TODO: Add pagination, filtering, and sorting
}

public class DeveloperService(IMapper mapper, NetLinkDbContext dbContext) : IDeveloperService
{
    public async Task<Guid> AddDeveloperAsync(DeveloperDto developerDto)
    {
        await CheckIfDeveloperExistsAsync(developerDto.Username!);

        var developer = mapper.Map<Developer>(developerDto);
        developer.DevToken = GenerateDevToken();
        dbContext.Developers.Add(developer);
        await dbContext.SaveChangesAsync();

        return developer.Id;
    }

    public async Task ValidateDeveloperAsync(string devToken)
    {
        var developer = await dbContext.Developers.FirstOrDefaultAsync(d => d.DevToken == devToken);

        if (developer == null)
            throw new NotFoundException(
                "Developer with this token does not exist or DevToken is invalid. Please check your DevToken in the configuration file.");

        if (!developer.Active)
            throw new DeveloperException("Developer account is deactivated. Please contact support.");

        if (developer.DeletedAt != null)
            throw new DeveloperException("Developer account has been deleted. If you think this is a mistake, please contact support.");
    }

    public async Task<Guid> GetDeveloperIdFromTokenAsync(string devToken)
    {
        var developer = await dbContext.Developers.FirstOrDefaultAsync(d => d.DevToken == devToken);

        if (developer == null)
            throw new DeveloperException(
                "Developer with this token does not exist or DevToken is invalid. Please check your DevToken in the configuration file.");

        return developer.Id;
    }

    public async Task<DeveloperRes> GetDeveloperByIdAsync(Guid id)
    {
        var developer = await FindDeveloperByIdAsync(id);
        return mapper.Map<DeveloperRes>(developer);
    }

    public async Task<DeveloperRes> GetDeveloperByUsernameAsync(string username) =>
        await dbContext.Developers.FirstOrDefaultAsync(d => d.Username == username) is { } developer
            ? mapper.Map<DeveloperRes>(developer)
            : throw new NotFoundException("Developer with this username does not exist.");

    public async Task<DeveloperRes> UpdateDeveloperAsync(Guid id, DeveloperDto developerDto)
    {
        var developer = await FindDeveloperByIdAsync(id);

        mapper.Map(developerDto, developer);
        await dbContext.SaveChangesAsync();

        return mapper.Map<DeveloperRes>(developer);
    }

    public async Task DeactivateDeveloperAsync(Guid id)
    {
        var developer = await FindDeveloperByIdAsync(id);
        developer.Active = false;
        await dbContext.SaveChangesAsync();
    }

    public async Task ReactivateDeveloperAsync(Guid id)
    {
        var developer = await FindDeveloperByIdAsync(id);
        developer.Active = true;
        await dbContext.SaveChangesAsync();
    }

    public async Task SoftDeleteDeveloperAsync(Guid id)
    {
        var developer = await FindDeveloperByIdAsync(id);
        developer.DeletedAt = DateTime.Now;
        await dbContext.SaveChangesAsync();
    }

    public async Task RestoreDeveloperAsync(Guid id)
    {
        var developer = await FindDeveloperByIdAsync(id);
        developer.DeletedAt = null;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteDeveloperAsync(Guid id)
    {
        var developer = await FindDeveloperByIdAsync(id);
        dbContext.Developers.Remove(developer);
        await dbContext.SaveChangesAsync();
    }

    public Task<List<DeveloperRes>> ListDevelopersAsync()
    {
        return dbContext.Developers
            .Select(d => mapper.Map<DeveloperRes>(d))
            .ToListAsync();
    }

    private async Task<Developer> FindDeveloperByIdAsync(Guid id)
    {
        var developer = await dbContext.Developers.FirstOrDefaultAsync(d => d.Id == id);

        if (developer == null)
            throw new NotFoundException("Developer with this Id does not exist.");

        return developer;
    }

    private async Task CheckIfDeveloperExistsAsync(string username)
    {
        if (await dbContext.Developers.AnyAsync(d => d.Username == username))
            throw new DeveloperException("Developer with this username already exists. Please use another account.");
    }

    private string GenerateDevToken()
    {
        const string prefix = "NL";

        var randomBytes = new byte[64];
        RandomNumberGenerator.Fill(randomBytes);

        var base64String = Convert.ToBase64String(randomBytes);

        base64String = base64String.Replace("+", "")
            .Replace("/", "")
            .Replace("=", "");

        return $"{prefix}{base64String}";
    }
}