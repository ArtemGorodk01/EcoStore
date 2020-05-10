using EcoStore.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ef = EcoStore.EFCore.Entities;

namespace EcoStore.Web.Interfaces.Services
{
    public interface IOrderService
    {
        Task<bool> MakeOrder(Cart cart, int userId);
        Task<ef.Order> GetOrder(int orderId);
        Task<List<ef.ProductOrder>> GetProductOrdersByOrderId(int orderId);
        Task<ef.User> GetUserByOrderId(int orderId);
        Task<List<ef.Order>> GetUserOrders(string userLogin);
        Task<List<ef.Order>> GetOrders(int page, int countPerPage);
        Task<List<ef.Order>> GetNotProcessedOrders(int page, int countPerPage);
        Task<bool> MarkOrderAsProcessed(int orderId);
        Task<int> GetPagesCountNotProcessed(int countPerPage);
        Task<int> GetPagesCount(int countPerPage);
    }
}
