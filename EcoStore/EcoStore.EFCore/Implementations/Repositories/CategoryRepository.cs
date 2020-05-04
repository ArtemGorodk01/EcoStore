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

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Category.ToListAsync();
        }
    }
}
