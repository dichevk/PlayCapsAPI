using Microsoft.EntityFrameworkCore;
using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;

namespace PlayCapsViewer.Services
{
    public class PlayerService : IPlayerService
    {
        private DataContext _context;
        public PlayerService(DataContext context)
        {
            _context = context;
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            var newPlayer = await _context.Players.AddAsync(player);
            if (newPlayer != null)
            {
                await _context.SaveChangesAsync();
                return newPlayer.Entity;
            }
            return null;
        }

        public Task<bool> DeletePlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await _context.Players.FirstOrDefaultAsync(x => x.Id == id);   
        }

        public async Task<ICollection<Player>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        public Task<Player> UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
