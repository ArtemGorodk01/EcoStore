using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoStore.EFCore.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByLogin(string login);
        Task<List<User>> GetAllUsers();
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(string login);
    }
}
