using EcoStore.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoStore.EFCore.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int page, int count);
        Task<List<Product>> GetProductsByCategory(string categoryName, int page, int count);
        Task<Product> GetProductById(int productId);
        Task<List<UserMarkProduct>> GetProductReviews(int productId);
        Task SaveReview(UserMarkProduct userMarkProduct);
        Task<bool> AddProduct(Product product);
        Task<bool> EditProduct(Product product);
        Task<bool> DeleteProduct(int productId);
        Task<int> GetPagesCount(int perPage);
        Task<Product> GetProductByTitle(string title);
        Task<bool> DeleteReview(int userMarkProductId);
        Task<List<UserMarkProduct>> GetReviews(int page, int reviewsPerPage);
        Task<int> GetReviewCount(int reviewsPerPage);
    }
}
