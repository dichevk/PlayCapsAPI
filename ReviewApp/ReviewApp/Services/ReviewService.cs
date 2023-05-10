using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;

namespace PlayCapsViewer.Services
{
    public class ReviewService : IReviewerService
    {
        private DataContext _context;
        public ReviewService(DataContext context)
        {
            _context = context;
        }

    }
}
