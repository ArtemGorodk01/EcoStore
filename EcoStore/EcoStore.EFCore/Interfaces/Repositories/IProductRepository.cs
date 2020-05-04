using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoStore.EFCore.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int page, int count);

        Task<List<Product>> GetProductsByCategory(string categoryName, int page, int count);
    }
}
