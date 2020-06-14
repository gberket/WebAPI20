using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI20.Models.Entities;

namespace WebAPI20.Data.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetUser(int id);
        Task<User> AddUser(User user);
    }
}
