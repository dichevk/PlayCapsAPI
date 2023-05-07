using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<ICollection<PlayCap>> GetPlayCapByCategory(int categoryId);
        bool CategoryExists(int id);
        Task<bool> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(Category category);
    }
}
