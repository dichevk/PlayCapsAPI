using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface IReviewService
    {
        Task<Review> CreateReview(Review review);
        Task<Review> UpdateReview(Review review);
        Task<Review> GetReviewById(int reviewId);
        Task<Review> GetReviewByName(string name);
        Task<bool> DeleteReview(int reviewId);
    }
}
