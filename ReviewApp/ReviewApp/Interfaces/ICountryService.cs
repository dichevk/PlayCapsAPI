using PlayCapsViewer.Models;

namespace PlayCapsViewer.Interfaces
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountries();
        Task<Country> GetCountry(int countryId);
        Task<Country> CreateCountry(Country country);
        Task<Country> UpdateCountry(Country country);
        Task<bool> DeleteCountry(int countryId); 
        Task<Country> GetCountryOfPlayer(int playerId);
        Task<Country> GetCountryOfReviewer(int reviewerId);


    }
}
