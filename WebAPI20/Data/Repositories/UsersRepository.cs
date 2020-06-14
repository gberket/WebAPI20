using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI20.Data.Interfaces;
using WebAPI20.Models.Entities;

namespace WebAPI20.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private AppDBContext _dbContext;

        public UsersRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUser(User user)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == user.Id);
            if (result == null)
            {
                var newMovie = _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == user.Id);
            }
            return null;
        }

        public async Task<User> GetUser(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}
