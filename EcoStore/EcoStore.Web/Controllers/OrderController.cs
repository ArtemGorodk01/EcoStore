using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoStore.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private const int _ordersPerPage = 20;
        private IOrderService _orderService;
        private IProductService _productService;

        public OrderController(IOrderService orderService,
                               IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/list/{page}/{notProcessed}")]
        [HttpGet]
        public async Task<IActionResult> List(int page, bool notProcessed)
        {
            if (notProcessed)
            {
                ViewBag.List = await _orderService.GetNotProcessedOrders(page, _ordersPerPage);
                ViewBag.Pages = await _orderService.GetPagesCountNotProcessed(_ordersPerPage);
                ViewBag.NotProcessed = true;
            }
            else
            {
                ViewBag.List = await _orderService.GetOrders(page, _ordersPerPage);
                ViewBag.Pages = await _orderService.GetPagesCount(_ordersPerPage);
                ViewBag.NotProcessed = false;
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/{orderId}")]
        public async Task<IActionResult> Details(int orderId)
        {
            var poList = await _orderService.GetProductOrdersByOrderId(orderId);
            var dictionary = new Dictionary<string, int>();
            poList.ForEach(po => dictionary.Add(_productService.GetProductById(po.ProductId ?? 0).GetAwaiter().GetResult().Title, po.Amount ?? 0));
            ViewBag.Dictionary = dictionary;
            ViewBag.User = await _orderService.GetUserByOrderId(orderId);
            ViewBag.Order = await _orderService.GetOrder(orderId);
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string login)
        {
            ViewBag.List = await _orderService.GetUserOrders(login);
            ViewBag.Pages = 0;
            ViewBag.NotProcessed = true;
            return View("List");
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            await _orderService.MarkOrderAsProcessed(orderId);
            return Redirect("~/order/list/1/false");
        }
    }
}