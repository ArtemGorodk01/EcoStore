using EcoStore.EFCore.Context;
using EcoStore.EFCore.Entities;
using EcoStore.EFCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoStore.EFCore.Implementations.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private EcoStoreContext _context = new EcoStoreContext();

        public async Task AddOrder(int userId, decimal price, Dictionary<int, int> productIdAmount)
        {
            var order = new Order
            {
                Userd = userId,
                Status = false,
                Price = price,
                OrderDate = DateTime.Now
            };

            var count = await _context.Order.CountAsync();
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            _context.ProductOrder.AddRange(productIdAmount.Select(item => new ProductOrder
            {
                ProductId = item.Key,
                Amount = item.Value,
                OrderId = count + 1,
            }).ToList());
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(int orderId)
        {
            return await _context.Order.FirstOrDefaultAsync(o => o.Id.Equals(orderId));
        }

        public async Task<List<ProductOrder>> GetProductOrdersByOrderId(int orderId)
        {
            return await _context.ProductOrder.Where(po => po.OrderId.Equals(orderId))
                                              .ToListAsync();
        }

        public async Task<User> GetUserByOrderId(int orderId)
        {
            var userId = (await _context.Order.FirstOrDefaultAsync(o => o.Id.Equals(orderId)))
                         .Userd ?? 0;
            return await _context.User.FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public async Task<List<Order>> GetUserOrders(string userLogin)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Login.Equals(userLogin));
            return await _context.Order.Where(o => o.Userd.Equals(user.Id)).ToListAsync();
        }

        public async Task<List<Order>> GetOrders(int page, int countPerPage)
        {
            return await _context.Order.OrderByDescending(o => o.OrderDate)
                                       .Skip((page - 1) * countPerPage).Take(countPerPage)
                                       .ToListAsync();
        }

        public async Task<List<Order>> GetNotProcessedOrders(int page, int countPerPage)
        {
            return await _context.Order.Where(o => !(o.Status ?? false))
                                       .OrderByDescending(o => o.OrderDate)
                                       .Skip((page - 1) * countPerPage).Take(countPerPage)
                                       .ToListAsync();
        }

        public async Task<bool> MarkOrderAsProcessed(int orderId)
        {
            var order = await _context.Order.FirstOrDefaultAsync(o => o.Id.Equals(orderId));
            if (order == null)
            {
                return false;
            }

            order.Status = true;
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetPagesCount(int countPerPages)
        {
            return await _context.Order.CountAsync() / countPerPages + 1;
        }

        public async Task<int> GetNotProcessedPagesCount(int countPerPage)
        {
            return await _context.Order.Where(o => !(o.Status ?? false)).CountAsync() / countPerPage + 1;
        }
    }
}
