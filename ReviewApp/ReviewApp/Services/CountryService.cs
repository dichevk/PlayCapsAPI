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

        public Task<Country> CreateCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Country>> GetCountries()
        {
            throw new NotImplementedException();
        }

        public Task<Country> GetCountry(int countryId)
        {
            throw new NotImplementedException();
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
