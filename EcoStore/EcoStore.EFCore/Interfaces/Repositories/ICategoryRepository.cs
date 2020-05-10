using EcoStore.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoStore.EFCore.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<bool> AddCategory(Category category);
        Task<bool> EditCategory(Category category);
        Task<bool> DeleteCategory(Category category);
        Task<Category> GetCategoryByTitle(string title);
    }
}
