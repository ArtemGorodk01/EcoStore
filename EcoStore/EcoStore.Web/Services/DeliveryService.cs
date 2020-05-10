using EcoStore.EFCore.Entities;
using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoStore.Web.Services
{
    public class DeliveryService : IDeliveryService
    {
        private IEcoStoreUnitOfWork _unitOfWork;

        public DeliveryService(IEcoStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CompleteDelivery(int deliveryId)
        {
            return await _unitOfWork.DeliveryRepository.CompleteDelivery(deliveryId);
        }

        public async Task<bool> CreateDelivery(Delivery delivey)
        {
            return await _unitOfWork.DeliveryRepository.CreateDelivery(delivey);
        }

        public async Task<List<Delivery>> GetDeliveries(int page, int perPage)
        {
            return await _unitOfWork.DeliveryRepository.GetDeliveries(page, perPage);
        }

        public async Task<Delivery> GetDeliveryById(int deliveryId)
        {
            return await _unitOfWork.DeliveryRepository.GetDeliveryById(deliveryId);
        }

        public async Task<List<Delivery>> GetNotComletedDeliveries(int page, int perPage)
        {
            return await _unitOfWork.DeliveryRepository.GetNotComletedDeliveries(page, perPage);
        }

        public async Task<int> GetNotCompletedPages(int perPage)
        {
            return await _unitOfWork.DeliveryRepository.GetNotCompletedPages(perPage);
        }

        public async Task<int> GetPages(int perPage)
        {
            return await _unitOfWork.DeliveryRepository.GetPages(perPage);
        }
    }
}
