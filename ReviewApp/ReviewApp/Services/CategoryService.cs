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

        public bool CreateCategory(Category category)
        {
            _dataContext.Categories.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _dataContext?.Categories.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _dataContext.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _dataContext.Categories.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<PlayCap> GetPlayCapByCategory(int categoryId)
        {
            return _dataContext.PlayCapsCategories.Where(x => x.CategoryId == categoryId).Select(x => x.PlayCap).ToList();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            if (saved > 0)
                return true;
            return false; 
        }

        public bool UpdateCategory(Category category)
        {
           _dataContext.Categories.Update(category);
            return Save();
        }
    }
}
