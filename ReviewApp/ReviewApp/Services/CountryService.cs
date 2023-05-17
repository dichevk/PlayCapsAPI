using Microsoft.EntityFrameworkCore;
using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;

namespace PlayCapsViewer.Services
{
    public class CountryService : ICountryService
    {
        private DataContext _context;
        public CountryService(DataContext context)
        {
            _context = context;
        }

        public async Task<Country> CreateCountry(Country country)
        {
            var countryAdded = await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return countryAdded.Entity;
        }

        public async Task<bool> DeleteCountry(int countryId)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == countryId);
            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Country>> GetCountries()
        {
            var countries = await _context.Countries.ToListAsync();
            return countries;
        }

        public async Task<Country> GetCountry(int countryId)
        {
            return await _context.Countries.FirstOrDefaultAsync(x => x.Id == countryId);
        }

        public Task<Country> GetCountryOfPlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public Task<Country> GetCountryOfReviewer(int reviewerId)
        {
            throw new NotImplementedException();
        }

        public Task<Country> UpdateCountry(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
