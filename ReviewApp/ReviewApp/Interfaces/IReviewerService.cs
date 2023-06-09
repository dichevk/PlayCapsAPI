﻿using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface IReviewerService
    {
        Task<Reviewer> CreateReviewer(Reviewer reviewer);
        Task<Reviewer> UpdateReviewer(int reviewerId, Reviewer reviewer);
        Task<Reviewer> GetReviewerById(int reviewerId);
        Task<Reviewer> GetReviewerByName(string name);
        Task<bool> DeleteReviewer(int reviewerId);
        Task<ICollection<Reviewer>> GetAllReviewers();
    }
}
