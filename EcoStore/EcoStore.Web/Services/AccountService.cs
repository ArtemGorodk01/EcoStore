using EcoStore.EFCore.Entities;
using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Enums;
using EcoStore.Web.Interfaces.Services;
using EcoStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Services
{
    public class AccountService : IAccountService
    {
        private IEcoStoreUnitOfWork _unitOfWork;

        public AccountService(IEcoStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddNewAccount(RegisterModel user, Role role)
        {
            if (user == null)
            {
                return false;
            }

            //TODO Add model validation
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllUsers();
                var id = users == null || users.Count == 0 ?
                         1 :
                         users.Max(u => u.Id) + 1;
                var newUser = new User
                {
                    Id = id,
                    Role = (int)role,
                    Login = user.Login,
                    Phone = user.Phone,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Country = user.Country,
                    Region = user.Region,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    RegistrationDate = DateTime.Now,
                    Gender = (int)user.Gender,
                };

                return await _unitOfWork.UserRepository.AddUser(newUser);
            }
            catch
            {
                return false;
            }
        }

        public async Task<Role> Login(LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return Role.None;
            }

            var user = await _unitOfWork.UserRepository.GetUserByLogin(loginModel.Login);
            if (user == null)
            {
                return Role.None;
            }

            return user.Password == loginModel.Password ?
                   (Role)user.Role :
                   Role.None;
        }

        public async Task<AccountInfo> GetAccountInfoByLogin(string login)
        {
            if (login == null)
            {
                return null;
            }

            var user = await _unitOfWork.UserRepository.GetUserByLogin(login);
            if (user == null)
            {
                return null;
            }

            return new AccountInfo
            {
                Login = user.Login,
                Phone = user.Phone,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                Region = user.Region,
                Address = user.Address,
            };
        }
    }
}
