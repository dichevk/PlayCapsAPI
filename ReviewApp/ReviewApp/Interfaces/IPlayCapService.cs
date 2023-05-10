using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface IPlayCapService
    {
        Task<ICollection<PlayCap>> GetPlayCapsByCategory(int categoryId);
        Task<ICollection<PlayCap>> GetAllPlayCaps();
        Task<PlayCap> GetPlayCap(int playCapId);
        Task<ICollection<PlayCap>> GetPlayCapsForPlayer(int playerId);
        Task<PlayCap> UpdatePlayCap(PlayCap playCap);
        Task<PlayCap> CreatePlayCap(PlayCap playCap, Player player);
        Task<bool> DeletePlayCap(int playCapId);
        Task<decimal> GetPlayCapRating(int playCapId);

    }
}
