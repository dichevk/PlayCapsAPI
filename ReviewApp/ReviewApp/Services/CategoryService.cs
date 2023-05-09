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

        public async Task<Category> CreateCategory(Category category)
        {
            
            var createdCategory = await _dataContext.Categories.AddAsync(category);
            await _dataContext.SaveChangesAsync();
            return createdCategory.Entity;
            
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                var categoryToDelete = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
                if (categoryToDelete != null)
                {
                    _dataContext.Categories.Remove(categoryToDelete);
                    await _dataContext.SaveChangesAsync();
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
            var categories = await _dataContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }

        public async Task<ICollection<Category>> GetCategoriesByPlayCapId(int playCapId)
        {
            var playCapCategories = await _dataContext.PlayCapsCategories.Where(x => x.PlayCapId == playCapId).Select(x => x.Category).ToListAsync();
            return playCapCategories;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var updatedCategory = _dataContext.Categories.Update(category);
            await _dataContext.SaveChangesAsync();
            return updatedCategory.Entity;
        }

    }
}
