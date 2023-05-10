using Microsoft.EntityFrameworkCore;
using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;

namespace PlayCapsViewer.Services
{
    public class PlayCapService : IPlayCapService
    {
        private DataContext _context;
        public PlayCapService(DataContext context)
        {
            _context = context;
        }

        public Task<PlayCap> CreatePlayCap(PlayCap playCap, Player player)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePlayCap(int playCapId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<PlayCap>> GetAllPlayCaps()
        {
            throw new NotImplementedException();
        }

        public Task<PlayCap> GetPlayCap(int playCapId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetPlayCapRating(int playCapId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PlayCap>> GetPlayCapsByCategory(int categoryId)
        {
            var playCaps = await _context.PlayCapsCategories.Where(x => x.CategoryId == categoryId).Select(x => x.PlayCap).ToListAsync();
            return playCaps;
        }

        public Task<ICollection<PlayCap>> GetPlayCapsForPlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public Task<PlayCap> UpdatePlayCap(PlayCap playCap)
        {
            throw new NotImplementedException();
        }
    }
}
