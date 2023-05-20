using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;

namespace PlayCapsViewer.Services
{
    public class ReviewerService : IReviewerService
    {
        private DataContext _context;
        public ReviewerService(DataContext context)
        {
            _context = context;
        }

        public Task<Reviewer> CreateReviewer(Reviewer reviewer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReviewer(int reviewerId)
        {
            throw new NotImplementedException();
        }

        public Task<Reviewer> GetReviewerById(int reviewerId)
        {
            throw new NotImplementedException();
        }

        public Task<Reviewer> GetReviewerByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Reviewer> UpdateReviewer(Reviewer reviewer)
        {
            throw new NotImplementedException();
        }
    }
}
