using EcoStore.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoStore.Web.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> GetProductById(int productId);
        Task<List<UserReview>> GetReviewsByProductId(int productId);
        Task SaveReview(UserReview review);
        Task<List<EFCore.Entities.Product>> GetProducts(int page, int count);
        Task<List<EFCore.Entities.Product>> GetProducts();
        Task<bool> DeleteProduct(int productId);
        Task<bool> AddProduct(EFCore.Entities.Product product);
        Task<bool> EditProduct(EFCore.Entities.Product product);
        Task<int> GetPagesCount(int perPage);
        Task<EFCore.Entities.Product> GetProduct(int productId);
        Task<EFCore.Entities.Product> GetProductByTitle(string productTitle);
    }
}
