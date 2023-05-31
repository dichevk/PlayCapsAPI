using PlayCapsViewer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApp.Test.Services
{
    public class CountryServiceTestcs
    {
        private CountryService _countryService;
        public async void Init()
        {
            var dbContext = new DbContextTestSetup();
            var dbSetup = await dbContext.GetDatabaseContext();
            _countryService = new CountryService(dbSetup);
        }
        [Fact]
        public async void CountryService_GetAllCountries_ReturnsListOfCountries()
        {
            //Arrange
            Init();
            //Act
            var result = await _countryService.GetCountries();
            //Assert
            result.Should().NotBeEmpty();
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Country>>();
        }
        [Fact]
        public async void CountryService_GetCountry_ReturnsCountry()
        {
            //Arrange
            Init();
            //Act
            var result = await _countryService.GetCountry(1);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Country>();
        }
        [Fact]
        public async void CountryService_GetCountry_WithInvalidId_ReturnsNull()
        {
            //Arrange
            Init();
            //Act
            var result = await _countryService.GetCountry(-231);
            //Assert
            result.Should().BeNull();
        }
    }
}
