using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<ICollection<Category>> GetCategoriesByPlayCapId(int playCapId);
        bool CategoryExists(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int categoryId);
    }
}
