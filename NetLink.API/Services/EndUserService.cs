using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.DTOs;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Services;

public interface IEndUserService
{
    Task<string> AddEndUserAsync(EndUserDto endUserDTO, string devToken);
    Task<bool> CheckIfEndUserExistsAsync(string endUserId);
    Task<EndUserDto> GetUserByIdAsync(string endUserId);
}

public class EndUserService : IEndUserService
{
    private readonly IMapper _mapper;
    private readonly NetLinkDbContext _dbContext;

    public EndUserService(IMapper mapper, NetLinkDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<string> AddEndUserAsync(EndUserDto endUserDto, string devToken)
    {
        var developerId = await GetDeveloperFromTokenAsync(devToken);
        var endUserExists = await CheckIfEndUserExistsAsync(endUserDto.Id!);
        if (endUserExists)
            throw new EndUserException("EndUser already exists, please use another account.");

        var endUser = _mapper.Map<EndUser>(endUserDto);
        _dbContext.EndUsers.Add(endUser);

        var developerUser = new DeveloperUser
        {
            DeveloperId = developerId,
            EndUserId = endUser.Id
        };
        _dbContext.DeveloperUsers.Add(developerUser);
        await _dbContext.SaveChangesAsync();

        return endUser.Id!;
    }

    public async Task<EndUserDto> GetUserByIdAsync(string endUserId)
    {
        if (endUserId == null)
            throw new NotFoundException("End user not found.");

        var endUser = await _dbContext.EndUsers.FirstOrDefaultAsync(endUser => endUser.Id == endUserId);

        return _mapper.Map<EndUserDto>(endUser);
    }

    private async Task<Guid> GetDeveloperFromTokenAsync(string devToken)
    {
        var developer = await _dbContext.Developers.FirstOrDefaultAsync(d => d.DevToken == devToken);

        if (developer == null)
            throw new DeveloperException("Developer with this token does not exist or DevToken is invalid, please check your DevToken in configuration file.");
        return developer.Id;
    }

    public async Task<bool> CheckIfEndUserExistsAsync(string endUserId)
    {
        var existingEndUser = await _dbContext.EndUsers.FirstOrDefaultAsync(e => e.Id == endUserId);
        return existingEndUser != null;
    }
}