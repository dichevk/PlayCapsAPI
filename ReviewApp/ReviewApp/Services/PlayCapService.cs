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

        public async Task<PlayCap> CreatePlayCap(PlayCap playCap, int playerId, int categoryId)
        {
            var playCapPlayerEntity = await _context.Players.Where(a => a.Id == playerId).FirstOrDefaultAsync();
            var category = await _context.Categories.Where(a => a.Id == categoryId).FirstOrDefaultAsync();
            var newPlayCapsPlayer = new PlayCapsPlayer()
            {
                Player = playCapPlayerEntity,
                PlayCap = playCap,
            };
            await _context.AddAsync(newPlayCapsPlayer);
            var playCapCategory = new PlayCapsCategory()
            {
                PlayCap = playCap,
                Category = category
            };

            await _context.AddAsync(playCap);
            await _context.SaveChangesAsync();
            return playCap;
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

        public async Task<ICollection<PlayCap>> GetAllPlayCaps()
        {
            return await _context.PlayCaps.ToListAsync();
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

        public async Task<decimal> GetPlayCapRating(int playCapId)
        {
            var playCapReview = await _context.Reviews.Where(x => x.PlayCap.Id == playCapId).FirstOrDefaultAsync();
            if (playCapReview.Rating <= 0)
            {
                return 0;
            }
            else
            {
                return playCapReview.Rating;
            }
        }

        public async Task<ICollection<PlayCap>> GetPlayCapsByCategory(int categoryId)
        {
            var playCaps = await _context.PlayCapsCategories.Where(x => x.CategoryId == categoryId).Select(x => x.PlayCap).ToListAsync();
            return playCaps;
        }

        public async Task<ICollection<PlayCap>> GetPlayCapsForPlayer(int playerId)
        {
            var playCapsList = new List<PlayCap>();
            var playerEntities = await _context.PlayCapsPlayers.Where(x => x.PlayerId == playerId).ToListAsync();
            foreach (var playerEntity in playerEntities)
            {
                playCapsList.Add(playerEntity.PlayCap);
            }
            return playCapsList;
        }

        public async Task<PlayCap> UpdatePlayCap(PlayCap playCap)
        {
            _context.Update(playCap);
            await _context.SaveChangesAsync();
            return playCap;
        }
    }
}
