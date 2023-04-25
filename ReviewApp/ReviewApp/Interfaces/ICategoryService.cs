using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface ICategoryService
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<PlayCap> GetPlayCapByCategory(int categoryId);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
