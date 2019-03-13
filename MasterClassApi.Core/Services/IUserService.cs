using MasterClassApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MasterClassApi.Core.Services
{
    public interface IUserService
    {
        string Authenticate(string username, string password);
        User Get(int id);
        Task<User> GetAsync(int id);
    }
}
