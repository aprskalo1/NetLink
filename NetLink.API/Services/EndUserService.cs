using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.DTOs;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Services
{
    public interface IEndUserService
    {
        Task<string> AddEndUserAsync(EndUserDTO endUserDTO, string devToken);
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

        public async Task<string> AddEndUserAsync(EndUserDTO endUserDTO, string devToken)
        {
            var developerId = await GetDeveloperFromToken(devToken);
            await CheckIfEndUserExistsAsync(endUserDTO.Id!);
            
            var endUser = _mapper.Map<EndUser>(endUserDTO);
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

        private async Task<Guid> GetDeveloperFromToken(string devToken)
        {
            var developer = await _dbContext.Developers.FirstOrDefaultAsync(d => d.DevToken == devToken);
            if (developer == null)
                throw new DevTokenException("Developer with this token does not exist or DevToken is invalid, please check your DevToken in configuration file.");
            return developer.Id;
        }

        private async Task CheckIfEndUserExistsAsync(string endUserId)
        {
            var existingEndUser = await _dbContext.EndUsers.FirstOrDefaultAsync(e => e.Id == endUserId);
            if (existingEndUser != null)
                throw new EndUserException("EndUser with this ID already exists, please use another ID.");
        }
    }
}
