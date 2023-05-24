using Microsoft.EntityFrameworkCore;
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

        public async Task<Review> CreateReview(Review review)
        {
            await _context.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReview(int reviewId)
        {
            var foundReview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (foundReview != null)
            {
                _context.Remove(foundReview);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ICollection<Review>> GetAllReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<ICollection<Review>> GetAllReviewsByPlayCap(int playCapId)
        {
            return await _context.Reviews.Where(x => x.PlayCap.Id == playCapId).ToListAsync();
        }

        public async Task<ICollection<Review>> GetAllReviewsByReviewer(int reviewerId)
        {
            return await _context.Reviews.Where(x => x.Reviewer.Id == reviewerId).ToListAsync();
        }

        public async Task<ICollection<Review>> GetAllReviewsForPlayCapByReviewer(int reviewerId, int playCapId)
        {
            return await _context.Reviews.Where(r => r.Reviewer.Id == reviewerId && r.PlayCap.Id == playCapId).ToListAsync();
        }

        public async Task<Review> GetReviewById(int reviewId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
        }

        public async Task<Review> GetReviewByName(string name)
        {
            return await _context.Reviews.Where(x => (string)x.Reviewer.FirstName.Concat(" ").Concat(x.Reviewer.LastName) == name).FirstOrDefaultAsync();
        }

        public async Task<Review> GetReviewByReviewerId(int reviewerId, int reviewId)
        {
            return await _context.Reviews.Where(x => x.Reviewer.Id == reviewerId && x.Id == reviewId).FirstOrDefaultAsync();
        }

        public async Task<Review> UpdateReview(Review review)
        {
            var foundReview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == review.Id);
            if (foundReview != null)
            {
                _context.Update(review);
                var updated = await _context.SaveChangesAsync();
                if (updated > 0)
                {
                    return review;
                }
            }
            return null;
        }

    }
}
