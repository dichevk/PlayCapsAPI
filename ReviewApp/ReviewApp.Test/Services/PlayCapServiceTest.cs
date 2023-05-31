using PlayCapsViewer.Data.Enums;
using PlayCapsViewer.Services;
namespace ReviewApp.Test.Services
{
    public class PlayCapServiceTest
    {
        PlayCapService _playCapService;
        public async void Init()
        {
            var dbContext = new DbContextTestSetup();
            var dbSetup = await dbContext.GetDatabaseContext();
            _playCapService = new PlayCapService(dbSetup);
        }
        [Fact]
        public async void PlayCapService_GetAllPlayCaps_ReturnsListOfPlayCaps()
        {
            //Arrange
            Init();
            //Act 
            var result = await _playCapService.GetAllPlayCaps();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<PlayCap>>();
        }
        [Fact]
        public async void PlayCapService_GetPlayCapById_ReturnsPlayCap()
        {
            //Arrange
            Init();
            //Act 
            var result = await _playCapService.GetPlayCap(1);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<PlayCap>();
        }
        [Fact]
        public async void PlayCapService_InvalidGetPlayCapById_ReturnsNull()
        {
            //Arrange
            Init();
            //Act 
            var result = await _playCapService.GetPlayCap(100000000);
            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void PlayCapService_GetPlayCapByName_ReturnsPlayCap()
        {
            //Arrange
            var name = "Pikachu Tazo";
            Init();
            //Act 
            var result = await _playCapService.GetPlayCapByName(name);
            //Assert
            result.Should().BeOfType<PlayCap>();
            result.Should().NotBeNull();
        }

        [Fact]
        public async void PlayCapService_GetPlayCapByNameWrongInput_ReturnsNull()
        {
            //Arrange
            var name = "Nonexistententry";
            Init();
            //Act 
            var result = await _playCapService.GetPlayCapByName(name);
            //Assert
            result.Should().BeNull();
        }
    }
}
