using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Enums;
using EcoStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Interfaces.Services
{
    public interface IAccountService
    {
        Task<bool> AddNewAccount(RegisterModel user, Role role);
        Task<Role> Login(LoginModel loginModel);
        Task<AccountInfo> GetAccountInfoByLogin(string login);
    }
}
