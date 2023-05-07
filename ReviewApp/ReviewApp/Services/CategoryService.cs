using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;
using PlayCapsViewer.Data;
using Microsoft.EntityFrameworkCore;

namespace PlayCapsViewer.Services
{
    public class CategoryService : ICategoryService
    {

        private DataContext _dataContext;

        //get category, delete category, create category create a category for a playcap, update category, get all categories, get a playcap by its category 

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CategoryExists(int id)
        {
            //if the category exists, then for any of the cateogires the id will be found
            return _dataContext.Categories.Any(x => x.Id == id); //returns true if the category exists, false otherwise 
        }

        public async Task<bool> CreateCategory(Category category)
        {
            try
            {
                await _dataContext.Categories.AddAsync(category);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false; 
            }
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            try
            {
                _dataContext.Categories.Remove(category);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ICollection<Category>> GetCategories()
        {
            var categories = await _dataContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }

        public async Task<ICollection<PlayCap>> GetPlayCapByCategory(int categoryId)
        {
            var playCap = await _dataContext.PlayCapsCategories.Where(x => x.CategoryId == categoryId).Select(x => x.PlayCap).ToListAsync();
            return playCap;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var updatedCategory = _dataContext.Categories.Update(category);
            await _dataContext.SaveChangesAsync();
            return updatedCategory.Entity;
        }

    }
}
