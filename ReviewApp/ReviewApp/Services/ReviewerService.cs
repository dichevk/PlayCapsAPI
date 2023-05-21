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

        public async Task<Reviewer> CreateReviewer(Reviewer reviewer)
        {
            var value = await _context.Reviewer.AddAsync(reviewer);
        }

        public Task<bool> DeleteReviewer(int reviewerId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Reviewer>> GetAllReviewers()
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
