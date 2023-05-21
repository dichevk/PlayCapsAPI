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

        public Task<PlayCap> CreatePlayCap(PlayCap playCap, int playerId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletePlayCap(int playCapId)
        {
            var foundPlayCap = await _context.PlayCaps.FirstOrDefaultAsync(x => x.Id == playCapId);
            if (foundPlayCap != null)
            {
                _context.Remove(foundPlayCap);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<ICollection<PlayCap>> GetAllPlayCaps()
        {
            throw new NotImplementedException();
        }

        public async Task<PlayCap?> GetPlayCap(int playCapId)
        {
            var foundPlayCap = await _context.PlayCaps.FirstOrDefaultAsync(x => x.Id == playCapId);
            if (foundPlayCap != null)
            {
                return foundPlayCap;
            }
            else
            {
                return null;
            }
        }

        public async Task<PlayCap?> GetPlayCapByName(string playCapName)
        {
            var foundPlayCap = await _context.PlayCaps.FirstOrDefaultAsync(x => x.Name == playCapName);
            if (foundPlayCap != null)
            {
                return foundPlayCap;
            }
            else
            {
                return null;
            }
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

        public Task<PlayCap> UpdatePlayCap(PlayCap playCap, int playerId, int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
