using EcoStore.EFCore.Entities;
using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private IEcoStoreUnitOfWork _unitOfWork;

        public CategoryService(IEcoStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCategory(Category category)
        {
            var existedCategory = await _unitOfWork.CategoryRepository.GetCategoryByTitle(category.Title);
            if (existedCategory != null)
            {
                return false;
            }

            return await _unitOfWork.CategoryRepository.AddCategory(category);
        }

        public async Task<bool> EditCategory(Category category)
        {
            return await _unitOfWork.CategoryRepository.EditCategory(category);
        }

        public async Task<bool> DeleteCategory(string title)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByTitle(title);
            if (category == null)
            {
                return false;
            }

            return await _unitOfWork.CategoryRepository.DeleteCategory(category);
        }

        public async Task<List<string>> GetAllCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategories();
            return categories.Select(c => c.Title).ToList();
        }

        public async Task<Category> GetCategoryByTitle(string title)
        {
            return await _unitOfWork.CategoryRepository.GetCategoryByTitle(title);
        }
    }
}
