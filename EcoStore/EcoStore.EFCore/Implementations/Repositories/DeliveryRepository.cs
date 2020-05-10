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
    public class DeliveryRepository : IDeliveryRepository
    {
        private EcoStoreContext _context = new EcoStoreContext();

        public async Task<bool> CompleteDelivery(int deliveryId)
        {
            var delivery = await GetDeliveryById(deliveryId);
            if (delivery != null)
            {
                delivery.Status = 1;
                _context.Delivery.Update(delivery);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CreateDelivery(Delivery delivery)
        {
            if (delivery == null)
            {
                return false;
            }

            _context.Delivery.Add(delivery);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Delivery>> GetDeliveries(int page, int perPage)
        {
            return await _context.Delivery
                .OrderByDescending(d => d.DeliveryDate)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();
        }

        public async Task<Delivery> GetDeliveryById(int deliveryId)
        {
            return await _context.Delivery.FirstOrDefaultAsync(d => d.Id.Equals(deliveryId));
        }

        public async Task<List<Delivery>> GetNotComletedDeliveries(int page, int perPage)
        {
            return await _context.Delivery.Where(d=>d.Status == null || d.Status.Equals(0))
                .OrderByDescending(d => d.DeliveryDate)
                .Skip((page-1)*perPage)
                .Take(perPage)
                .ToListAsync();
        }

        public async Task<int> GetPages(int perPage)
        {
            return await _context.Delivery.CountAsync() / perPage + 1;
        }

        public async Task<int> GetNotCompletedPages(int perPage)
        {
            return await _context.Delivery.Where(d => d.Status == null || d.Status.Equals(0))
                .CountAsync() / perPage + 1;
        }
    }
}
