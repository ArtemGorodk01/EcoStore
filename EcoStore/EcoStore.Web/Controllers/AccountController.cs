using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Enums;
using EcoStore.Web.Interfaces.Services;
using EcoStore.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userLogin = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await _accountService.GetAccountInfoByLogin(userLogin);
            return View(user);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userLogin = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await _accountService.GetAccountInfoByLogin(userLogin);
            return View(user);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(AccountInfo accountInfo)
        {
            await _accountService.UpdateUser(accountInfo);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]/{login}")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string login)
        {
            var user = await _accountService.GetUserByLogin(login);
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/save")]
        [HttpGet]
        public async Task<IActionResult> EditUser(EcoStore.EFCore.Entities.User user)
        {
            await _accountService.UpdateUser(user);
            return Redirect("~/account/list/1");
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/Search")]
        [HttpGet]
        public async Task<IActionResult> FindUser(string login)
        {
            var list = new List<string>();
            var user = await _accountService.GetUserByLogin(login);
            if (user != null)
            {
                list.Add(user.Login);
            }

            ViewBag.List = list;
            ViewBag.Pages = 0;
            return View("List");
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/AddUser")]
        [HttpGet]
        public async Task<IActionResult> AddUser(EFCore.Entities.User user)
        {
            await _accountService.AddUser(user);
            return Redirect("~/account/list/1");
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/DeleteUser/{login}")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string login)
        {
            await _accountService.DeleteUser(login);
            return Redirect("~/account/list/1");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var role = await _accountService.Login(model);
            if (!role.Equals(Role.None))
            {
                await Authenticate(model.Login, role);
                if (role.Equals(Role.Admin))
                {
                    return RedirectToAction("Admin", "Home");
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _accountService.AddNewAccount(model, Role.User))
                {
                    await Authenticate(model.Login, Role.User);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Некорректные данные");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "Admin")]
        [Route("[Controller]/[action]/{page}")]
        [HttpGet]
        public async Task<IActionResult> List(int page)
        {
            var pages = await _accountService.GetPagesCount();
            ViewBag.Pages = await _accountService.GetPagesCount();
            if (page < 1 || page > pages)
            {
                page = 1;
            }

            ViewBag.List = await _accountService.GetUsers(page);
            return View();
        }

        private async Task Authenticate(string userName, Role role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString())
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}