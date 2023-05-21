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
        Task<ICollection<Review>> GetAllReviews();
        Task<ICollection<Review>> GetAllReviewsByReviewer(int reviewerId);
        Task<ICollection<Review>> GetAllReviewsByPlayCap(int playCapId);
        Task<ICollection<Review>> GetAllReviewsForPlayCapByReviewer(int reviewerId, int playCapId);
    }
}
