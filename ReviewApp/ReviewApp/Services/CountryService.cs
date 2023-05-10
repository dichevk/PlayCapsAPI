using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;

namespace PlayCapsViewer.Services
{
    public class CountryService : ICountryService
    {
        private DataContext _context;
        public CountryService(DataContext context)
        {
            _context = context;
        }
    }
}
