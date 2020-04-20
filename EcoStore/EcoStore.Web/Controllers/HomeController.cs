using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoStore.Web.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoStore.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin, User")]
        public IActionResult Index()
        {
            return View();
        }
    }
}