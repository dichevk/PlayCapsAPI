using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface IPlayCapService
    {
        Task<ICollection<PlayCap>> GetPlayCapsByCategory(int categoryId);
    }
}
