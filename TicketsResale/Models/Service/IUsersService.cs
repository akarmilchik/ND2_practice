using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface IUsersService
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        IEnumerable<User> GetUsers();
    }
}
