using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface IPlayerService
    {
        Task<ICollection<Player>> GetPlayers();
        Task<Player?> GetPlayer(int id);
        Task<Player?> CreatePlayer(Player player);
        Task<Player?> UpdatePlayer(Player player);
        Task<bool> DeletePlayer(int playerId);
        Task<ICollection<PlayCap>> GetPlayCapByPlayer(int playerId);
    }
}
