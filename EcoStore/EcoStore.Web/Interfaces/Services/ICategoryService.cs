using EcoStore.EFCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoStore.Web.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(Category category);
        Task<bool> EditCategory(Category category);
        Task<bool> DeleteCategory(string category);
        Task<List<string>> GetAllCategories();
        Task<Category> GetCategoryByTitle(string title);
    }
}
