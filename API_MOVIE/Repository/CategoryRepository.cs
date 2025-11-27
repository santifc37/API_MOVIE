using Class_Programmation.DAL;
using Class_Programmation.DAL.Models;
using Class_Programmation.Repository.iRepository;
using Microsoft.EntityFrameworkCore;

namespace Class_Programmation.Repository
{

    public class CategoryRepository : ICategoryRepository
    {
        public readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
                _context = context;
        }
        public async Task<bool> CategoryExistByidAsync(int id)
        {
            var categoryExists=await _context.Categories
                .AsNoTracking()
                .AnyAsync(c=>c.id==id);

            return categoryExists;
        }

        public async Task<bool> CategoryExistByNameAsync(string name)
        {
            var categoryExists = await _context.Categories
                .AsNoTracking()
                .AnyAsync(c => c.name == name);

            return categoryExists;
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            category.CreatedDate = DateTime.UtcNow;
            
            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id);

            if (category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.name)
                .ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.id == id);
            return category;
        }

        public async Task<bool> updateCategoryAsync(Category category)
        {
            category.ModifiedDate = DateTime.UtcNow;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

    

