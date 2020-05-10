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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoStore.Web.Controllers
{
    public class CartController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;
        private IAccountService _accountService;

        public CartController(IProductService productService,
                              IOrderService orderService,
                              IAccountService accountService)
        {
            _productService = productService;
            _orderService = orderService;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Cart = GetCart();
            return View();
        }

        [Route("[controller]/add/{productId}")]
        [HttpGet]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var product = await _productService.GetProductById(productId);
            var cart = GetCart();
            cart.AddItem(product, 1);
            SetCart(cart);
            return Redirect($"/product/{productId}");
        }

        [Route("[controller]/delete/{productTitle}")]
        [HttpGet]
        public IActionResult DeleteCartLine(string productTitle)
        {
            var cart = GetCart();
            cart.RemoveProduct(productTitle);
            SetCart(cart);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        [Route("[controller]/order")]
        public async Task<IActionResult> Order()
        {
            var cart = GetCart();
            var userLogin = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await _accountService.GetAccountInfoByLogin(userLogin);
            ViewBag.Status = await _orderService.MakeOrder(cart, user.Id);
            return View();
        }

        public Cart GetCart()
        {
            Cart cart = null;
            try
            {
                cart = JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("Cart"));
            }
            catch { }
            finally
            {
                if (cart == null)
                {
                    cart = new Cart();
                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
                }
            }
            
            return cart;
        }

        public void SetCart(Cart cart)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
    }
}
