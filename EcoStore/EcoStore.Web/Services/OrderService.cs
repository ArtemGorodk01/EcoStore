using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Interfaces.Services;
using EcoStore.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Services
{
    public class OrderService : IOrderService
    {
        private IEcoStoreUnitOfWork _unitOfWork;

        public OrderService(IEcoStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> MakeOrder(Cart cart, int userId)
        {
            try
            {
                Dictionary<int, int> dictionary = new Dictionary<int, int>();
                cart.Lines.ToList().ForEach(l => dictionary.Add(l.Product.Id, l.Amount));
                await _unitOfWork.OrderRepository.AddOrder(userId, cart.ComputeTotalValue(), dictionary);
            }
            catch 
            {
                return false;
            }

            return true;
        }

        public async Task<EFCore.Entities.Order> GetOrder(int orderId)
        {
            return await _unitOfWork.OrderRepository.GetOrder(orderId);
        }

        public async Task<List<EFCore.Entities.ProductOrder>> GetProductOrdersByOrderId(int orderId)
        {
            return await _unitOfWork.OrderRepository.GetProductOrdersByOrderId(orderId);
        }

        public async Task<EFCore.Entities.User> GetUserByOrderId(int orderId)
        {
            return await _unitOfWork.OrderRepository.GetUserByOrderId(orderId);
        }

        public async Task<List<EFCore.Entities.Order>> GetUserOrders(string userLogin)
        {
            return await _unitOfWork.OrderRepository.GetUserOrders(userLogin);
        }

        public async Task<List<EFCore.Entities.Order>> GetOrders(int page, int countPerPage)
        {
            return await _unitOfWork.OrderRepository.GetOrders(page, countPerPage);
        }

        public async Task<List<EFCore.Entities.Order>> GetNotProcessedOrders(int page, int countPerPage)
        {
            return await _unitOfWork.OrderRepository.GetNotProcessedOrders(page, countPerPage);
        }

        public async Task<bool> MarkOrderAsProcessed(int orderId)
        {
            return await _unitOfWork.OrderRepository.MarkOrderAsProcessed(orderId);
        }

        public async Task<int> GetPagesCount(int countPerPage)
        {
            return await _unitOfWork.OrderRepository.GetPagesCount(countPerPage);
        }

        public async Task<int> GetPagesCountNotProcessed(int countPerPage)
        {
            return await _unitOfWork.OrderRepository.GetNotProcessedPagesCount(countPerPage);
        }
    }
}
