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
    public class CategoryRepository : ICategoryRepository
    {
        private EcoStoreContext _context = new EcoStoreContext();

        public CategoryRepository()
        {
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<bool> AddCategory(Category category)
        {
            try
            {
                _context.Category.Add(category);
            }
            catch
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditCategory(Category category)
        {
            try
            {
                _context.Category.Update(category);
            }
            catch
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            try
            {
                _context.Category.Remove(category);
            }
            catch
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Category> GetCategoryByTitle(string title)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.Title.Equals(title));
        }
    }
}
