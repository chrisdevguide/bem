using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessEconomyManager.Data.Repositories.Interfaces;
using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessEconomyManager.Data.Repositories.Implementations
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public AppUserRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task AddAppUser(AppUser appUser)
        {
            _dataContext.AppUsers.Add(appUser);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> AppUserExists(string emailAddress)
        {
            return await _dataContext.AppUsers.AnyAsync(x => x.EmailAddress == emailAddress.ToLower());
        }

        public async Task<bool> AppUserExists(Guid id)
        {
            return await _dataContext.AppUsers.AnyAsync(x => x.Id == id);
        }

        public async Task<AppUser> GetAppUser(string emailAddress)
        {
            return await _dataContext.AppUsers
                .Include(x => x.Business)
                .SingleOrDefaultAsync(x => x.EmailAddress == emailAddress.ToLower());
        }

        public async Task<AppUserDto> GetAppUserDto(string emailAddress)
        {
            return await _dataContext.AppUsers
                .Include(x => x.Business)
                .ProjectTo<AppUserDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.EmailAddress == emailAddress.ToLower());
        }


    }
}
