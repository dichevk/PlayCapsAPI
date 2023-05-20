using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;

namespace PlayCapsViewer.Services
{
    public class ReviewService : IReviewService
    {
        private DataContext _context;
        public ReviewService(DataContext context)
        {
            _context = context;
        }

        public Task<Review> CreateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReview(int reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewById(int reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
