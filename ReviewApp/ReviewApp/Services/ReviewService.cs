using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;

namespace PlayCapsViewer.Services
{
    public class ReviewService : IReviewService
    {
        private DataContext _context;
        public ReviewService(DataContext context)
        {
            _context = context;
        }

    }
}
