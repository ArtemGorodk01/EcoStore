using EcoStore.CommonServices.MailSender;
using EcoStore.EFCore.Entities;
using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Enums;
using EcoStore.Web.Interfaces.Services;
using EcoStore.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Services
{
    public class AccountService : IAccountService
    {
        private const int _viewUserAmount = 20;

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
                var newUser = new EFCore.Entities.User
                {
                    Id = 0,
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

                if (await _unitOfWork.UserRepository.AddUser(newUser))
                {
                    var addedUser = await _unitOfWork.UserRepository.GetUserByLogin(user.Login);
                    await SmtpWorker.SendEmailAsync(newUser.Login, "Активация аккаунта", $"Для активации аккаунта перейдите по ссылке https://localhost:44319/Account/Activate/{newUser.Id}");
                    return true;
                }

                return false;
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
                Id = user.Id,
                Login = user.Login,
                Phone = user.Phone,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                Region = user.Region,
                Address = user.Address,
            };
        }

        public async Task UpdateUser(AccountInfo accountInfo)
        {
            var user = await _unitOfWork.UserRepository.GetUserByLogin(accountInfo.Login);
            user.Phone = accountInfo.Phone;
            user.FirstName = accountInfo.FirstName;
            user.LastName = accountInfo.LastName;
            user.Country = accountInfo.Country;
            user.Region = accountInfo.Region;
            user.Address = accountInfo.Address;
            await _unitOfWork.UserRepository.UpdateUser(user);
        }

        public async Task UpdateUser(EFCore.Entities.User user)
        {
            await _unitOfWork.UserRepository.UpdateUser(user);
        }

        public async Task<List<string>> GetUsers(int page)
        {
            var users = await _unitOfWork.UserRepository.GetAllUsers();
            return users.Skip((page - 1) * _viewUserAmount).Take(_viewUserAmount)
                        .Select(u => u.Login).ToList();
        }

        public async Task<int> GetPagesCount()
        {
            return (await _unitOfWork.UserRepository.GetUsersCount())/_viewUserAmount + 1;
        }

        public async Task<EFCore.Entities.User> GetUserByLogin(string login)
        {
            return await _unitOfWork.UserRepository.GetUserByLogin(login);
        }

        public async Task<bool> AddUser(EFCore.Entities.User user)
        {
            user.RegistrationDate = DateTime.Now;
            await SmtpWorker.SendEmailAsync(user.Login, "Shalom", "hi");
            return await _unitOfWork.UserRepository.AddUser(user);
        }

        public async Task<bool> DeleteUser(string login)
        {
            return await _unitOfWork.UserRepository.DeleteUser(login);
        }

        public async Task<bool> ActivateUser(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(userId);
            user.Status = 1;
            return await _unitOfWork.UserRepository.UpdateUser(user);
        }
    }
}
