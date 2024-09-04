using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Repositories;

public interface IDeveloperRepository
{
    Task AddDeveloperAsync(Developer developer);
    Task<Developer> GetDeveloperByIdAsync(Guid developerId);
    Task<Developer> GetDeveloperByTokenAsync(string devToken);
    Task<Developer> GetDeveloperByUsernameAsync(string username);
    Task<List<Developer>> ListDevelopersAsync();
    Task UpdateDeveloperAsync(Developer developer);
    Task DeleteDeveloperAsync(Developer developer);
    Task SaveChangesAsync();
    Task<bool> CheckIfDeveloperExistsAsync(string username);
}

public class DeveloperRepository(NetLinkDbContext dbContext) : IDeveloperRepository
{
    public async Task AddDeveloperAsync(Developer developer)
    {
        await dbContext.Developers.AddAsync(developer);
    }

    public async Task<Developer> GetDeveloperByIdAsync(Guid developerId)
    {
        var developer = await dbContext.Developers.FirstOrDefaultAsync(d => d.Id == developerId);
        return developer ?? throw new NotFoundException($"Developer with ID {developerId} not found.");
    }

    public async Task<Developer> GetDeveloperByTokenAsync(string devToken)
    {
        var developer = await dbContext.Developers.FirstOrDefaultAsync(d => d.DevToken == devToken);
        return developer ?? throw new NotFoundException($"Developer with token {devToken} not found.");
    }

    public async Task<Developer> GetDeveloperByUsernameAsync(string username)
    {
        var developer = await dbContext.Developers.FirstOrDefaultAsync(d => d.Username == username);
        return developer ?? throw new NotFoundException($"Developer with username {username} not found.");
    }

    public async Task<List<Developer>> ListDevelopersAsync()
    {
        return await dbContext.Developers.ToListAsync();
    }

    public async Task UpdateDeveloperAsync(Developer developer)
    {
        dbContext.Developers.Update(developer);
        await SaveChangesAsync();
    }

    public async Task DeleteDeveloperAsync(Developer developer)
    {
        dbContext.Developers.Remove(developer);
        await SaveChangesAsync();
    }

    public async Task<bool> CheckIfDeveloperExistsAsync(string username)
    {
        return await dbContext.Developers.AnyAsync(d => d.Username == username);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}