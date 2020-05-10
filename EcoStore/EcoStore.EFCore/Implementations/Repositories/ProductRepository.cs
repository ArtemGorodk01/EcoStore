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

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Product.Where(p => p.Id.Equals(productId)).FirstOrDefaultAsync();
        }

        public async Task<List<UserMarkProduct>> GetProductReviews(int productId)
        {
            return await _context.UserMarkProduct.Where(r => int.Equals(r.ProductId, productId))
                                                 .ToListAsync();
        }

        public async Task SaveReview(UserMarkProduct userMarkProduct)
        {
            _context.UserMarkProduct.Add(userMarkProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddProduct(Product product)
        {
            if (product == null)
            {
                return false;
            }

            var existedUser = _context.Product.FirstOrDefault(p => p.Title.Equals(product.Title));
            if (existedUser != null)
            {
                return false;
            }

            try
            {
                _context.Product.Add(product);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> EditProduct(Product product)
        {
            if (product == null)
            {
                return false;
            }

            var existedUser = _context.Product.FirstOrDefault(p => p.Title.Equals(product.Title));
            if (existedUser != null)
            {
                if (!existedUser.Id.Equals(product.Id))
                {
                    return false;
                }
            }

            try
            {
                _context.Product.Update(product);
                await _context.SaveChangesAsync();
            }
            catch 
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var existedUser = _context.Product.FirstOrDefault(p => p.Id.Equals(productId));
            if (existedUser == null)
            {
                return false;
            }

            try
            {
                _context.Product.Remove(existedUser);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<int> GetPagesCount(int perPage)
        {
            return await _context.Product.CountAsync() / perPage + 1;
        }

        public async Task<Product> GetProductByTitle(string title)
        {
            return await _context.Product.FirstOrDefaultAsync(p => p.Title.Equals(title));
        }

        public async Task<bool> DeleteReview(int userMarkProductId)
        {
            var userMarkProduct = await _context.UserMarkProduct.FirstOrDefaultAsync(r => r.Id.Equals(userMarkProductId));
            if (userMarkProduct == null)
            {
                return false;
            }

            try
            {
                _context.UserMarkProduct.Remove(userMarkProduct);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<List<UserMarkProduct>> GetReviews(int page, int reviewsPerPage)
        {
            if (page < 1 || reviewsPerPage < 1)
            {
                return new List<UserMarkProduct>();
            }

            return await _context.UserMarkProduct.Skip((page - 1) * reviewsPerPage).Take(reviewsPerPage).ToListAsync();

        }

        public async Task<int> GetReviewCount(int reviewsPerPage)
        {
            return await _context.UserMarkProduct.CountAsync()/reviewsPerPage + 1;
        }
    }
}
