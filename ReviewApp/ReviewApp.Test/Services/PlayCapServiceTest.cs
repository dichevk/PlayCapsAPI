using PlayCapsViewer.Services;
namespace ReviewApp.Test.Services
{
    public class PlayCapServiceTest
    {
        [Fact]
        public async void PlayCapService_GetAllPlayCaps_ReturnsListOfPlayCaps()
        {
            //Arrange
            var dbContext = new DbContextTestSetup();
            var dbSetup = await dbContext.GetDatabaseContext();
            var playCapService = new PlayCapService(dbSetup);
            //Act 
            var result = await playCapService.GetAllPlayCaps();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<PlayCap>>();
        }
        [Fact]
        public async void PlayCapService_GetPlayCapById_ReturnsPlayCap()
        {
            //Arrange
            var dbContext = new DbContextTestSetup();
            var dbSetup = await dbContext.GetDatabaseContext();
            var playCapService = new PlayCapService(dbSetup);
            //Act 
            var result = await playCapService.GetPlayCap(1);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<PlayCap>();
        }
        [Fact]
        public async void PlayCapService_InvalidGetPlayCapById_ReturnsNull()
        {
            //Arrange
            var dbContext = new DbContextTestSetup();
            var dbSetup = await dbContext.GetDatabaseContext();
            var playCapService = new PlayCapService(dbSetup);
            //Act 
            var result = await playCapService.GetPlayCap(100000000);
            //Assert
            result.Should().BeNull();
        }
    }
}
