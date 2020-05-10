using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EcoStore.Web.Interfaces.Services;
using EcoStore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private const int _productCountPerPage = 20;
        private IProductService _productService;
        private IAccountService _accountService;

        public ProductController(IProductService productService,
                                 IAccountService accountService)
        {
            _productService = productService;
            _accountService = accountService;
        }

        [Route("[controller]/{productId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int productId)
        {
            var product = await _productService.GetProductById(productId);
            var reviews = await _productService.GetReviewsByProductId(productId);
            ViewBag.Product = product;
            ViewBag.Reviews = reviews;
            return View();
        }

        [Authorize(Roles = "User")]
        [Route("[controller]/{productId}/review")]
        [HttpGet]
        public async Task<IActionResult> AddReview(UserReview userReview)
        {
            var userLogin = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await _accountService.GetAccountInfoByLogin(userLogin);
            userReview.UserId = user.Id;
            await _productService.SaveReview(userReview);
            return Redirect($"~/product/{userReview.ProductId}");
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]/{page}")]
        public async Task<IActionResult> List(int page)
        {
            var pages = await _productService.GetPagesCount(_productCountPerPage);
            var list = await _productService.GetProducts(page, _productCountPerPage);
            ViewBag.Pages = pages;
            ViewBag.List = list;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(EFCore.Entities.Product product)
        {
            await _productService.AddProduct(product);
            return Redirect("~/product/list/1");
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]/{productId}")]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productService.GetProduct(productId);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(EFCore.Entities.Product product)
        {
            await _productService.EditProduct(product);
            return Redirect("~/product/list/1");
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]/{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            await _productService.DeleteProduct(productId);
            return Redirect("~/product/list/1");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string title)
        {
            var list = new List<EFCore.Entities.Product>();
            var product = await _productService.GetProductByTitle(title);
            if (product != null)
            {
                list.Add(product);
            }

            ViewBag.List = list;
            ViewBag.Pages = 0;
            return View("List");
        }
    }
}