using Microsoft.EntityFrameworkCore;
using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;
using System.Runtime.InteropServices;

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

        public async Task<Country> GetCountryOfPlayer(int playerId)
        {
            var foundPlayer = await _context.Players.FirstOrDefaultAsync(x => x.Id==playerId);
            if (foundPlayer!=null)
            {
                return foundPlayer.Country;
            }
            return null;
        }

        public async Task<Country> GetCountryOfReviewer(int reviewerId)
        {
            var foundReviewer = await _context.Reviewer.FirstOrDefaultAsync(x => x.Id == reviewerId);
            return null;
        }

        public async Task<Country> UpdateCountry(Country country)
        {
            var updatedCountry =  _context.Countries.Update(country);
            await _context.SaveChangesAsync();
            return updatedCountry.Entity;
        }
    }
}
