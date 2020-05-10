using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoStore.EFCore.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrder(int userId, decimal price, Dictionary<int, int> productIdAmount);
        Task<Order> GetOrder(int orderId);
        Task<List<ProductOrder>> GetProductOrdersByOrderId(int orderId);
        Task<User> GetUserByOrderId(int orderId);
        Task<List<Order>> GetUserOrders(string userLogin);
        Task<List<Order>> GetOrders(int page, int countPerPage);
        Task<List<Order>> GetNotProcessedOrders(int page, int countPerPage);
        Task<bool> MarkOrderAsProcessed(int orderId);
        Task<int> GetPagesCount(int countPerPages);
        Task<int> GetNotProcessedPagesCount(int countPerPage);
    }
}
