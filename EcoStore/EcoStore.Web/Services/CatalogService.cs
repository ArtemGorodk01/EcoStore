using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Interfaces.Services;
using EcoStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private IEcoStoreUnitOfWork _unitOfWork;

        public CatalogService(IEcoStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategories();
            return categories.Select(c => new Category()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
            }).ToList();
        }

        public async Task<List<Product>> GetProducts(int page, int count)
        {
            var products = await _unitOfWork.ProductRepository.GetProducts(page, count);
            return products.Select(p => new Product 
            {
            Id = p.Id,
            CategoryId = p.CategoryId ?? 0,
            Title = p.Title,
            GuaranteeMonth = p.GuaranteeMonth ?? 0,
            Price = p.Price ?? 0,
            Description = p.Description,
            ImageData = $"data:image/jpeg;base64,{p.ImageDataBase64}",
            }).ToList();
        }

        public async Task<List<Product>> GetProductsByCategory(string category, int page, int count)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsByCategory(category, page, count);
            return products.Select(p => new Product
            {
                Id = p.Id,
                CategoryId = p.CategoryId ?? 0,
                Title = p.Title,
                GuaranteeMonth = p.GuaranteeMonth ?? 0,
                Price = p.Price ?? 0,
                Description = p.Description,
                ImageData = $"data:image/jpeg;base64,{p.ImageDataBase64}",
            }).ToList();
        }
    }
}
