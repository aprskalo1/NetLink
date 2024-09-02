using System.Security.Cryptography;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Services;

public interface IDeveloperService
{
    Task<Guid> AddDeveloperAsync(DeveloperRequestDto developerRequestDto);
    Task ValidateDeveloperAsync(string devToken);
    Task<Guid> GetDeveloperIdFromTokenAsync(string devToken);
    Task<DeveloperResponseDto> GetDeveloperByIdAsync(Guid developerId);
    Task<DeveloperResponseDto> GetDeveloperByUsernameAsync(string username);
    Task<DeveloperResponseDto> UpdateDeveloperAsync(Guid developerId, DeveloperRequestDto developerRequestDto);
    Task DeactivateDeveloperAsync(Guid developerId);
    Task ReactivateDeveloperAsync(Guid developerId);
    Task SoftDeleteDeveloperAsync(Guid developerId);
    Task RestoreDeveloperAsync(Guid developerId);
    Task DeleteDeveloperAsync(Guid developerId);
    Task<List<DeveloperResponseDto>> ListDevelopersAsync(); //TODO: Add pagination, filtering, and sorting
}

public class DeveloperService(IMapper mapper, NetLinkDbContext dbContext) : IDeveloperService
{
    public async Task<Guid> AddDeveloperAsync(DeveloperRequestDto developerRequestDto)
    {
        await CheckIfDeveloperExistsAsync(developerRequestDto.Username!);

        var developer = mapper.Map<Developer>(developerRequestDto);
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

    public async Task<DeveloperResponseDto> GetDeveloperByIdAsync(Guid developerId)
    {
        var developer = await FindDeveloperByIdAsync(developerId);
        return mapper.Map<DeveloperResponseDto>(developer);
    }

    public async Task<DeveloperResponseDto> GetDeveloperByUsernameAsync(string username) =>
        await dbContext.Developers.FirstOrDefaultAsync(d => d.Username == username) is { } developer
            ? mapper.Map<DeveloperResponseDto>(developer)
            : throw new NotFoundException("Developer with this username does not exist.");

    public async Task<DeveloperResponseDto> UpdateDeveloperAsync(Guid developerId, DeveloperRequestDto developerRequestDto)
    {
        var developer = await FindDeveloperByIdAsync(developerId);

        mapper.Map(developerRequestDto, developer);
        await dbContext.SaveChangesAsync();

        return mapper.Map<DeveloperResponseDto>(developer);
    }

    public async Task DeactivateDeveloperAsync(Guid developerId)
    {
        var developer = await FindDeveloperByIdAsync(developerId);
        developer.Active = false;
        await dbContext.SaveChangesAsync();
    }

    public async Task ReactivateDeveloperAsync(Guid developerId)
    {
        var developer = await FindDeveloperByIdAsync(developerId);
        developer.Active = true;
        await dbContext.SaveChangesAsync();
    }

    public async Task SoftDeleteDeveloperAsync(Guid developerId)
    {
        var developer = await FindDeveloperByIdAsync(developerId);
        developer.DeletedAt = DateTime.Now;
        await dbContext.SaveChangesAsync();
    }

    public async Task RestoreDeveloperAsync(Guid developerId)
    {
        var developer = await FindDeveloperByIdAsync(developerId);
        developer.DeletedAt = null;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteDeveloperAsync(Guid developerId)
    {
        var developer = await FindDeveloperByIdAsync(developerId);
        dbContext.Developers.Remove(developer);
        await dbContext.SaveChangesAsync();
    }

    public Task<List<DeveloperResponseDto>> ListDevelopersAsync()
    {
        return dbContext.Developers
            .Select(d => mapper.Map<DeveloperResponseDto>(d))
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

    private static string GenerateDevToken()
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