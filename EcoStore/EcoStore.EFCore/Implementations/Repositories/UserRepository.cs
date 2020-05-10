using EcoStore.EFCore.Context;
using EcoStore.EFCore.Entities;
using EcoStore.EFCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoStore.EFCore.Implementations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private EcoStoreContext _context = new EcoStoreContext();

        public async Task<bool> AddUser(User user)
        {
            if (user == null)
            {
                return false;
            }

            var existedUser = await _context.User
                                    .SingleOrDefaultAsync(u => u.Login.Equals(user.Login));
            if (existedUser != null)
            {
                return false;
            }

            _context.User.Add(user);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(string login)
        {
            if (login == null)
            {
                return false;
            }

            var existedUser = await _context.User
                                    .SingleOrDefaultAsync(u => u.Login.Equals(login));
            if (existedUser == null)
            {
                return false;
            }

            _context.User.Remove(existedUser);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserByLogin(string login)
        {
            if (login == null)
            {
                return null;
            }

            return await _context.User
                         .SingleOrDefaultAsync(u => u.Login.Equals(login));
        }

        public async Task<bool> UpdateUser(User user)
        {
            if (user == null)
            {
                return false;
            }

            var existedUser = await _context.User
                                    .SingleOrDefaultAsync(u => u.Login.Equals(user.Login));
            if (existedUser == null)
            {
                return false;
            }

            //_context.User.Remove(existedUser);
            //_context.Add(user);
            _context.User.Update(user);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public async Task<int> GetUsersCount()
        {
            return await _context.User.CountAsync();
        }
    }
}
