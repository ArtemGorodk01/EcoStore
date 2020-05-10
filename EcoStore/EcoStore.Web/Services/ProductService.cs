using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Interfaces.Services;
using EcoStore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Services
{
    public class ProductService : IProductService
    {
        private IEcoStoreUnitOfWork _unitOfWork;

        public ProductService(IEcoStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Models.Product> GetProductById(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetProductById(productId);
            return product != null ?
            new Models.Product()
            {
                Id = product.Id,
                CategoryId = product.CategoryId ?? 0,
                Title = product.Title,
                GuaranteeMonth = product.GuaranteeMonth ?? 0,
                Price = product.Price ?? 0,
                Description = product.Description,
                ImageData = $"data:image/jpeg;base64,{product.ImageDataBase64}",
            } :
            null;
        }

        public async Task<List<EFCore.Entities.Product>> GetProducts(int page, int count)
        {
            var products = await _unitOfWork.ProductRepository.GetProducts(page, count);
            return products;
        }

        public async Task<bool> EditProduct(EFCore.Entities.Product product)
        {
            return await _unitOfWork.ProductRepository.EditProduct(product);
        }

        public async Task<bool> AddProduct(EFCore.Entities.Product product)
        {
            return await _unitOfWork.ProductRepository.AddProduct(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            return await _unitOfWork.ProductRepository.DeleteProduct(productId);
        }

        public async Task<EFCore.Entities.Product> GetProduct(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetProductById(productId);
            return product;
        }

        public async Task<List<UserReview>> GetReviewsByProductId(int productId)
        {
            var reviews = await _unitOfWork.ProductRepository.GetProductReviews(productId);
            return reviews.Select(r => new UserReview
            {
                ProductId = r.ProductId,
                UserId = r.UserId,
                UserName = GetUserById(r.UserId??0).GetAwaiter().GetResult().FirstName,
                Review = r.Review,
                Mark = r.Mark ?? 0,
            }).ToList();
        }

        public async Task SaveReview(UserReview userReview)
        {
            await _unitOfWork.ProductRepository.SaveReview(new EFCore.Entities.UserMarkProduct
            {
                //Id = new Random().Next(),
                ProductId = userReview.ProductId,
                UserId = userReview.UserId,
                Review = userReview.Review,
                Mark = userReview.Mark,
            });
        }

        public async Task<int> GetPagesCount(int perPage)
        {
            return await _unitOfWork.ProductRepository.GetPagesCount(perPage);
        }

        private async Task<Models.User> GetUserById(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(userId);
            return new Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Login = user.Login,
            };
        }

        public async Task<EFCore.Entities.Product> GetProductByTitle(string productTitle)
        {
            return await _unitOfWork.ProductRepository.GetProductByTitle(productTitle);
        }
    }
}
