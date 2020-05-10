using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using EcoStore.EFCore.Entities;
using EcoStore.Web.Interfaces.Services;
using EcoStore.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EcoStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            ViewBag.List = await _categoryService.GetAllCategories();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]/{title}")]
        [HttpGet]
        public async Task<IActionResult> Edit(string title)
        {
            var category = await _categoryService.GetCategoryByTitle(title);
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(Category category)
        {
            await _categoryService.EditCategory(category);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]/{title}")]
        public async Task<IActionResult> Delete(string title)
        {
            await _categoryService.DeleteCategory(title);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _categoryService.AddCategory(category);
            return RedirectToAction("List");
        }
    }
}