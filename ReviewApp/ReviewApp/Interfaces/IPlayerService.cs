using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface IPlayerService
    {
        Task<List<Player>> GetPlayers();
        Task<Player?> GetPlayer(int id);
        Task<Player?> GetPlayerOfPlayCap(int playCapId);
        Task<Player?> CreatePlayer(Player player);
        Task<Player?> UpdatePlayer(Player player);
        Task<bool> DeletePlayer(int playerId);
    }
}
