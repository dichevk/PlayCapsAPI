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
        public async Task<ICollection<PlayCap>> GetPlayCapsByCategory(int categoryId)
        {
            var playCaps = await _context.PlayCapsCategories.Where(x => x.CategoryId == categoryId).Select(x => x.PlayCap).ToListAsync();
            return playCaps;
        }
    }
}
