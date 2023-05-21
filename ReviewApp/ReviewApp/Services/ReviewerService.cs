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
            await _context.Reviewers.AddAsync(reviewer);
            await _context.SaveChangesAsync();
            return reviewer;
        }

        public async Task<bool> DeleteReviewer(int reviewerId)
        {
            var reviewer = await _context.Reviewers.Where(x => x.Id == reviewerId).FirstOrDefaultAsync();
            if (reviewer != null)
            {
                _context.Remove(reviewer);
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
            var foundReviewer = await _context.Reviewers.Where(x => (string)x.FirstName.Concat(" ").Concat(x.LastName) == name).FirstOrDefaultAsync();

            if (foundReviewer != null)
            {
                return foundReviewer;
            }
            else
            {
                return null;
            }
        }

        public async Task<Reviewer> UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            await _context.SaveChangesAsync();
            return reviewer;
        }
    }
}
