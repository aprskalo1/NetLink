using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.DTOs;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Services
{

    public interface IDevTokenService
    {
        Task<Guid> AddTokenAsync(DeveloperDTO developerDTO);
    }

    public class DevTokenService : IDevTokenService
    {
        private readonly IMapper _mapper;
        private readonly NetLinkDbContext _dbContext;

        public DevTokenService(IMapper mapper, NetLinkDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Guid> AddTokenAsync(DeveloperDTO developerDTO)
        {
            await CheckIfDeveloperExistsAsync(developerDTO.Username!);

            var developer = _mapper.Map<Developer>(developerDTO);
            _dbContext.Developers.Add(developer);
            await _dbContext.SaveChangesAsync();
            return developer.Id;
        }

        private async Task CheckIfDeveloperExistsAsync(string username)
        {
            var existingDeveloper = await _dbContext.Developers.FirstOrDefaultAsync(d => d.Username == username);
            if (existingDeveloper != null)
                throw new DevTokenException("Developer with this username already exists, please use another account.");
        }
    }
}
