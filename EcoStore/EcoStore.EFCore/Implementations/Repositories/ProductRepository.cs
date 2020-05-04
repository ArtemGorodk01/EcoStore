using EcoStore.EFCore.Context;
using EcoStore.EFCore.Entities;
using EcoStore.EFCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.EFCore.Implementations.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private EcoStoreContext _context = new EcoStoreContext();

        public async Task<List<Product>> GetProducts(int page, int count)
        {
            if (page < 1 || count < 1)
            {
                return new List<Product>();
            }

            return await _context.Product.Skip((page - 1) * count).Take(count).ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCategory(string categoryName, int page, int count)
        {
            if (categoryName == null || page < 1 || count < 1)
            {
                return new List<Product>();
            }

            return await _context.Product.Where(p => p.Category.Title.Equals(categoryName))
                                 .Skip((page - 1) * count).Take(count).ToListAsync();
        }
    }
}
