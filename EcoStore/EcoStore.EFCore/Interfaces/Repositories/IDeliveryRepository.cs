using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoStore.EFCore.Interfaces.Repositories
{
    public interface IDeliveryRepository
    {
        Task<List<Delivery>> GetDeliveries(int page, int perPage);
        Task<List<Delivery>> GetNotComletedDeliveries(int page, int perPage);
        Task<Delivery> GetDeliveryById(int deliveryId);
        Task<bool> CreateDelivery(Delivery delivey);
        Task<bool> CompleteDelivery(int deliveryId);
        Task<int> GetNotCompletedPages(int perPage);
        Task<int> GetPages(int perPage);
    }
}
