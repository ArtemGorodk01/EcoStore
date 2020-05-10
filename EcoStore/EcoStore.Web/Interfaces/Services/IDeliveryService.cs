using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ef = EcoStore.EFCore.Entities;

namespace EcoStore.Web.Interfaces.Services
{
    public interface IDeliveryService
    {
        Task<List<ef.Delivery>> GetDeliveries(int page, int perPage);
        Task<List<ef.Delivery>> GetNotComletedDeliveries(int page, int perPage);
        Task<ef.Delivery> GetDeliveryById(int deliveryId);
        Task<bool> CreateDelivery(ef.Delivery delivey);
        Task<bool> CompleteDelivery(int deliveryId);
        Task<int> GetNotCompletedPages(int perPage);
        Task<int> GetPages(int perPage);
    }
}
