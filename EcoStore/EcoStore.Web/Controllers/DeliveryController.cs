using System.Threading.Tasks;
using EcoStore.EFCore.Entities;
using EcoStore.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoStore.Web.Controllers
{
    public class DeliveryController : Controller
    {
        private const int _perPage = 20;
        private IDeliveryService _deliveryService;
        private IOrderService _orderService;

        public DeliveryController(IDeliveryService deliveryService,
            IOrderService orderService)
        {
            _deliveryService = deliveryService;
            _orderService = orderService;
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]/{orderId}")]
        [HttpGet]
        public async Task<IActionResult> Create(int orderId)
        {
            var order = await _orderService.GetOrder(orderId);
            var user = await _orderService.GetUserByOrderId(orderId);
            ViewBag.Order = order;
            ViewBag.User = user;
            var delivery = new Delivery
            {
                Id = 0,
                OrderId = order.Id,
                Address = user.Address,
                Status = 0,
            };

            return View(delivery);
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> Create(Delivery delivery)
        {
            await _deliveryService.CreateDelivery(delivery);
            await _orderService.MarkOrderAsProcessed(delivery.OrderId ?? 0);
            return Redirect("~/order/list/1/false");
        }

        [Authorize(Roles = "Delivery")]
        [Route("[controller]/[action]/{page}/{all}")]
        [HttpGet]
        public async Task<IActionResult> List(int page, bool all)
        {
            if (all)
            {
                ViewBag.List = await _deliveryService.GetDeliveries(page, _perPage);
                ViewBag.Pages = await _deliveryService.GetPages(_perPage);
            }
            else
            {
                ViewBag.List = await _deliveryService.GetNotComletedDeliveries(page, _perPage);
                ViewBag.Pages = await _deliveryService.GetNotCompletedPages(_perPage);
            }

            ViewBag.All = all;
            return View();
        }
    }
}