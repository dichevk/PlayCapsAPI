using PlayCapsViewer.Services;

namespace ReviewApp.Test.Services
{
    public class PlayerServiceTest
    {
        private DbContextTestSetup _testSetup;
        private PlayerService _playerService;
        async void Init()
        {
            _testSetup = new DbContextTestSetup();
            var testContext = await _testSetup.GetDatabaseContext();
            _playerService = new PlayerService(testContext);
        }
        [Fact]
        public async void PlayerService_GetAllPlayers_ReturnsListOfPlayers()
        {
            //Assert 
            Init();
            //Act 
            var result = await _playerService.GetPlayers();
            //Assert
            result.Should().NotBeEmpty();
            result.Should().BeOfType<List<Player>>();
        }
        [Fact]
        public async void PlayerService_GetPlayerById_ReturnsPlayer()
        {
            //Arrange
            Init();
            //Act 
            var result = await _playerService.GetPlayer(1);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Player>();
        }
        [Fact]
        public async void PlayerService_GetPlayerById_WithInvalidPlayerId_ReturnsNull()
        {
            //Arrange
            Init();
            //Act 
            var result = await _playerService.GetPlayer(-100000000);
            //Assert
            result.Should().BeNull();
        }
        [Fact]
        public async void PlayerService__ReturnsNull()
        {
            //Arrange
            Init();
            Player newPlayer = new Player()
            {
                Id = 1000670,
                Country = new Country() { Id = 10676800, Name = "Test" },
                FirstName = "John",
                LastName = "Samuel",
                Gym = "TestGym",
            };
            //Act 
            var result = await _playerService.CreatePlayer(newPlayer);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Player>();
        }
    }
}
