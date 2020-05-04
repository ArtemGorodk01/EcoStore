using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoStore.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoStore.Web.Controllers
{
    public class CatalogController : Controller
    {
        private const int _count = 9;
        private ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [Route("[controller]/{page}")]
        public async Task<IActionResult> Index(int page)
        {
            var categories = await _catalogService.GetCategories();
            var products = await _catalogService.GetProducts(page, _count);
            ViewBag.Next = false;
            ViewBag.Prev = false;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentCategory = "all";
            if (products.Count == _count)
            {
                ViewBag.Next = true;
            }

            if (page > 1)
            {
                ViewBag.Prev = true;
            }

            ViewBag.Categories = categories;
            ViewBag.Products = products;
            return View();
        }

        [Route("[controller]/{category}/{page}")]
        public async Task<IActionResult> Index(string category, int page)
        {
            if (category == "all")
            {
                return await Index(page);
            }

            var categories = await _catalogService.GetCategories();
            var products = await _catalogService.GetProductsByCategory(category, page, _count);
            ViewBag.Next = false;
            ViewBag.Prev = false;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentCategory = category;
            if (products.Count == _count)
            {
                ViewBag.Next = true;
            }

            if (page > 1)
            {
                ViewBag.Prev = true;
            }

            ViewBag.Categories = categories;
            ViewBag.Products = products;
            return View();
        }
    }
}