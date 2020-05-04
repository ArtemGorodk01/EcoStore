using EcoStore.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoStore.Web.Interfaces.Services
{
    public interface ICatalogService
    {
        Task<List<Category>> GetCategories();

        Task<List<Product>> GetProducts(int page, int count);

        Task<List<Product>> GetProductsByCategory(string category, int page, int count);
    }
}
