using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;
using PlayCapsViewer.Data;
using Microsoft.EntityFrameworkCore;

namespace PlayCapsViewer.Services
{
    public class CategoryService : ICategoryService
    {

        private DataContext _context;

        //get category, delete category, create category create a category for a playcap, update category, get all categories, get a playcap by its category 

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            //if the category exists, then for any of the cateogires the id will be found
            return _context.Categories.Any(x => x.Id == id); //returns true if the category exists, false otherwise 
        }

        public async Task<Category> CreateCategory(Category category)
        {
            
            var createdCategory = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return createdCategory.Entity;
            
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
                if (categoryToDelete != null)
                {
                    _context.Categories.Remove(categoryToDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ICollection<Category>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }

        public async Task<ICollection<Category>> GetCategoriesByPlayCapId(int playCapId)
        {
            var playCapCategories = await _context.PlayCapsCategories.Where(x => x.PlayCapId == playCapId).Select(x => x.Category).ToListAsync();
            return playCapCategories;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var updatedCategory = _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return updatedCategory.Entity;
        }

    }
}
