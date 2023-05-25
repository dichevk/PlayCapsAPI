using Microsoft.EntityFrameworkCore;
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
            var existingReviewer = await _context.Reviewers.Where(x => x.Id == reviewer.Id || (x.FirstName.Trim().ToLower() == reviewer.FirstName.Trim().ToLower() && x.LastName.Trim().ToLower() == reviewer.LastName.Trim().ToLower())).FirstOrDefaultAsync();
            if (existingReviewer != null) return null;
            await _context.Reviewers.AddAsync(reviewer);
            await _context.SaveChangesAsync();
            return reviewer;
        }

        public async Task<bool> DeleteReviewer(int reviewerId)
        {
            var reviewer = await _context.Reviewers.Where(x => x.Id == reviewerId).FirstOrDefaultAsync();
            if (reviewer != null)
            {
                try
                {
                    _context.Remove(reviewer);
                }
                catch
                {
                    throw new InvalidOperationException();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ICollection<Reviewer>> GetAllReviewers()
        {
            return await _context.Reviewers.ToListAsync();
        }

        public async Task<Reviewer> GetReviewerById(int reviewerId)
        {
            var foundReviewer = await _context.Reviewers.Where(x => x.Id == reviewerId).FirstOrDefaultAsync();
            if (foundReviewer != null)
            {
                return foundReviewer;
            }
            else
            {
                return null;
            }
        }

        public async Task<Reviewer> GetReviewerByName(string name)
        {
            var foundReviewer = await _context.Reviewers.Where(x => (string)x.FirstName.Trim().ToLower().Concat(x.LastName.Trim().ToLower()) == name.Trim().ToLower()).FirstOrDefaultAsync();

            if (foundReviewer != null)
            {
                return foundReviewer;
            }
            else
            {
                return null;
            }
        }

        public async Task<Reviewer> UpdateReviewer(int reviewerId, Reviewer reviewer)
        {
            var reviewerToUpdate = await _context.Reviewers.Where(x => x.Id == reviewerId).FirstOrDefaultAsync();
            if (reviewerToUpdate != null)
            {
                try
                {
                    _context.Update(reviewer);
                }
                catch
                {
                    throw new InvalidOperationException();
                }
                var updated = await _context.SaveChangesAsync();
                if (updated > 0)
                {
                    return reviewer;
                }
            }
            return null;
        }
    }
}
