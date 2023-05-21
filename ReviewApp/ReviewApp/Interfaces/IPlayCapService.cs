using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface IPlayCapService
    {
        Task<ICollection<PlayCap>> GetPlayCapsByCategory(int categoryId);
        Task<ICollection<PlayCap>> GetAllPlayCaps();
        Task<PlayCap> GetPlayCap(int playCapId);
        Task<PlayCap> GetPlayCapByName(string playCapName);
        Task<ICollection<PlayCap>> GetPlayCapsForPlayer(int playerId);
        Task<PlayCap> UpdatePlayCap(PlayCap playCap, int playerId, int categoryId);
        Task<PlayCap> CreatePlayCap(PlayCap playCap, int playerId, int categoryId);
        Task<bool> DeletePlayCap(int playCapId);
        Task<decimal> GetPlayCapRating(int playCapId);

    }
}
