using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;

namespace PlayCapsViewer.Services
{
    public class ReviewerService : IReviewerService
    {
        private DataContext _context;
        public ReviewerService(DataContext context)
        {
            _context = context;
        }
    }
}
