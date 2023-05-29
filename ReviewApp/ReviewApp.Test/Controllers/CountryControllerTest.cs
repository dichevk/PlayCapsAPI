
using Microsoft.AspNetCore.Http;

namespace ReviewApp.Test.Controllers
{
    public class CountryControllerTests
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private readonly CountryController _countryController;

        public CountryControllerTests()
        {
            _countryService = A.Fake<ICountryService>();
            _mapper = A.Fake<IMapper>();
            _countryController = new CountryController(_countryService, _mapper);
        }

        [Fact]
        public async Task GetCountries_ReturnsOkResultWithCountryDTOs()
        {
            // Arrange
            var countries = new List<Country> { new Country { Id = 1, Name = "Country 1" }, new Country { Id = 2, Name = "Country 2" } };
            var countryDTOs = new List<CountryDTO> { new CountryDTO { Id = 1, Name = "Country 1" }, new CountryDTO { Id = 2, Name = "Country 2" } };
            A.CallTo(() => _countryService.GetCountries()).Returns(countries);
            A.CallTo(() => _mapper.Map<List<CountryDTO>>(countries)).Returns(countryDTOs);

            // Act
            var result = await _countryController.GetCountries();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDTOs = Assert.IsAssignableFrom<List<CountryDTO>>(okResult.Value);
            Assert.Equal(countryDTOs.Count, returnedDTOs.Count);
            Assert.Equal(countryDTOs.First().Id, returnedDTOs.First().Id);
        }
        [Fact]
        public async Task GetCountryOfReviewer_WithValidReviewerId_ReturnsOkResultWithCountryDTO()
        {
            // Arrange
            int reviewerId = 1;
            Country country = new Country { Id = 1, Name = "Country 1" };
            CountryDTO countryDTO = new CountryDTO { Id = 1, Name = "Country 1" };

            A.CallTo(() => _countryService.GetCountryOfReviewer(reviewerId)).Returns(country);
            A.CallTo(() => _mapper.Map<CountryDTO>(country)).Returns(countryDTO);

            // Act
            IActionResult result = await _countryController.GetCountryOfReviewer(reviewerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(countryDTO);
        }


        [Fact]
        public async Task GetCountryOfPlayer_WithValidPlayerId_ReturnsOkResultWithCountryDTO()
        {
            // Arrange
            int playerId = 1;
            Country country = new Country { Id = 1, Name = "Country 1" };
            CountryDTO countryDTO = new CountryDTO { Id = 1, Name = "Country 1" };

            A.CallTo(() => _countryService.GetCountryOfPlayer(playerId)).Returns(country);
            A.CallTo(() => _mapper.Map<CountryDTO>(country)).Returns(countryDTO);

            // Act
            IActionResult result = await _countryController.GetCountryOfPlayer(playerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(countryDTO);
        }

        [Fact]
        public async Task CreateCountry_WithValidCountryDTO_ReturnsOkResultWithCreatedCountry()
        {
            // Arrange
            CountryDTO countryDTO = new CountryDTO { Name = "Country 1" };
            Country country = new Country { Id = 1, Name = "Country 1" };

            A.CallTo(() => _countryService.GetCountries()).Returns(new List<Country>());
            A.CallTo(() => _mapper.Map<Country>(countryDTO)).Returns(country);
            A.CallTo(() => _countryService.CreateCountry(country)).Returns(country);

            // Act
            IActionResult result = await _countryController.CreateCountry(countryDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(country);
        }
        [Fact]
        public async Task UpdateCountry_WithValidCountryIdAndCountryDTO_ReturnsOkResultWithUpdatedCountry()
        {
            // Arrange
            int countryId = 1;
            CountryDTO updatedCountryDTO = new CountryDTO { Id = 1, Name = "Updated Country 1" };
            Country existingCountry = new Country { Id = 1, Name = "Country 1" };
            Country updatedCountry = new Country { Id = 1, Name = "Updated Country 1" };

            A.CallTo(() => _countryService.GetCountry(countryId)).Returns(existingCountry);
            A.CallTo(() => _mapper.Map<Country>(updatedCountryDTO)).Returns(updatedCountry);
            A.CallTo(() => _countryService.UpdateCountry(updatedCountry)).Returns(updatedCountry);

            // Act
            IActionResult result = await _countryController.UpdateCountry(countryId, updatedCountryDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(updatedCountry);
        }

        [Fact]
        public async Task UpdateCountry_WithInvalidCountryId_ReturnsNotFoundResult()
        {
            // Arrange
            int countryId = 1;
            CountryDTO updatedCountryDTO = new CountryDTO { Id = 1, Name = "Updated Country 1" };
            A.CallTo(() => _countryService.GetCountry(countryId)).Returns(Task.FromResult<Country>(null));

            // Act
            IActionResult result = await _countryController.UpdateCountry(countryId, updatedCountryDTO);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteCountry_WithValidCountryId_ReturnsNoContentResult()
        {
            // Arrange
            int countryId = 1;
            A.CallTo(() => _countryService.DeleteCountry(countryId)).Returns(true);

            // Act
            IActionResult result = await _countryController.DeleteCountry(countryId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteCountry_WithInvalidCountryId_ReturnsNotFoundResult()
        {
            // Arrange
            int countryId = 1;
            A.CallTo(() => _countryService.DeleteCountry(countryId)).Returns(false);

            // Act
            IActionResult result = await _countryController.DeleteCountry(countryId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
