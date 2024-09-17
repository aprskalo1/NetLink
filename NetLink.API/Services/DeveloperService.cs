using System.Security.Cryptography;
using AutoMapper;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Repositories;

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

public class DeveloperService(IMapper mapper, IDeveloperRepository developerRepository) : IDeveloperService
{
    public async Task<Guid> AddDeveloperAsync(DeveloperRequestDto developerRequestDto)
    {
        if (await developerRepository.CheckIfDeveloperExistsAsync(developerRequestDto.Username!))
        {
            throw new DeveloperException($"Developer with username: {developerRequestDto.Username} already exists.");
        }

        var developer = mapper.Map<Developer>(developerRequestDto);
        developer.DevToken = GenerateDevToken();

        await developerRepository.AddDeveloperAsync(developer);
        await developerRepository.SaveChangesAsync();

        return developer.Id;
    }

    public async Task ValidateDeveloperAsync(string devToken)
    {
        var developer = await developerRepository.GetDeveloperByTokenAsync(devToken);

        if (!developer.Active)
        {
            throw new DeveloperException($"Developer account with devToken: {devToken} is deactivated.");
        }

        if (developer.DeletedAt != null)
        {
            throw new DeveloperException($"Developer account with devToken: {devToken} has been deleted.");
        }
    }

    public async Task<Guid> GetDeveloperIdFromTokenAsync(string devToken)
    {
        var developer = await developerRepository.GetDeveloperByTokenAsync(devToken);
        return developer.Id;
    }

    public async Task<DeveloperResponseDto> GetDeveloperByIdAsync(Guid developerId)
    {
        var developer = await developerRepository.GetDeveloperByIdAsync(developerId);

        return mapper.Map<DeveloperResponseDto>(developer);
    }

    public async Task<DeveloperResponseDto> GetDeveloperByUsernameAsync(string username)
    {
        var developer = await developerRepository.GetDeveloperByUsernameAsync(username);
        return mapper.Map<DeveloperResponseDto>(developer);
    }

    public async Task<DeveloperResponseDto> UpdateDeveloperAsync(Guid developerId, DeveloperRequestDto developerRequestDto)
    {
        var developer = await developerRepository.GetDeveloperByIdAsync(developerId);

        mapper.Map(developerRequestDto, developer);
        await developerRepository.UpdateDeveloperAsync(developer);

        return mapper.Map<DeveloperResponseDto>(developer);
    }

    public async Task DeactivateDeveloperAsync(Guid developerId)
    {
        var developer = await developerRepository.GetDeveloperByIdAsync(developerId);
        developer.Active = false;
        await developerRepository.SaveChangesAsync();
    }

    public async Task ReactivateDeveloperAsync(Guid developerId)
    {
        var developer = await developerRepository.GetDeveloperByIdAsync(developerId);
        developer.Active = true;
        await developerRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteDeveloperAsync(Guid developerId)
    {
        var developer = await developerRepository.GetDeveloperByIdAsync(developerId);
        developer.DeletedAt = DateTime.Now;
        await developerRepository.SaveChangesAsync();
    }

    public async Task RestoreDeveloperAsync(Guid developerId)
    {
        var developer = await developerRepository.GetDeveloperByIdAsync(developerId);
        developer.DeletedAt = null;
        await developerRepository.SaveChangesAsync();
    }

    public async Task DeleteDeveloperAsync(Guid developerId)
    {
        var developer = await developerRepository.GetDeveloperByIdAsync(developerId);
        await developerRepository.DeleteDeveloperAsync(developer);
    }

    public async Task<List<DeveloperResponseDto>> ListDevelopersAsync()
    {
        var developers = await developerRepository.ListDevelopersAsync();
        return mapper.Map<List<DeveloperResponseDto>>(developers);
    }

    private static string GenerateDevToken()
    {
        const string prefix = "NL";
        var randomBytes = new byte[64];
        RandomNumberGenerator.Fill(randomBytes);
        var base64String = Convert.ToBase64String(randomBytes)
            .Replace("+", "")
            .Replace("/", "")
            .Replace("=", "");

        return $"{prefix}{base64String}";
    }
}