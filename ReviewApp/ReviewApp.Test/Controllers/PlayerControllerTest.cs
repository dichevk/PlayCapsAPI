namespace ReviewApp.Test.Controllers
{
    public class PlayerControllerTest
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayerControllerTest()
        {
            _playerService = A.Fake<IPlayerService>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task GetPlayers_WithPlayers_ReturnsOkObjectResult()
        {
            // Arrange
            var players = new List<Player>();
            var playerDTOs = new List<PlayerDTO>();
            A.CallTo(() => _playerService.GetPlayers()).Returns(players);
            A.CallTo(() => _mapper.Map<List<PlayerDTO>>(players)).Returns(playerDTOs);
            var controller = new PlayerController(_playerService, _mapper);

            // Act
            var result = await controller.GetPlayers();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(playerDTOs);
        }


        [Fact]
        public async Task GetPlayer_WithExistingPlayer_ReturnsOkObjectResult()
        {
            // Arrange
            var playerId = 1;
            var player = new Player();
            var playerDTO = new PlayerDTO();
            A.CallTo(() => _playerService.GetPlayer(playerId)).Returns(player);
            A.CallTo(() => _mapper.Map<PlayerDTO>(player)).Returns(playerDTO);
            var controller = new PlayerController(_playerService, _mapper);

            // Act
            var result = await controller.GetPlayer(playerId);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(playerDTO);
        }






        [Fact]
        public async Task GetPlayers_WithNoPlayers_ReturnsNotFoundResult()
        {
            // Arrange
            List<Player> players = null;
            A.CallTo(() => _playerService.GetPlayers()).Returns(players);
            var controller = new PlayerController(_playerService, _mapper);

            // Act
            var result = await controller.GetPlayers();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("no players found");
        }

        [Fact]
        public async Task GetPlayCapPlayer_WithNonExistingPlayer_ReturnsNotFoundResult()
        {
            // Arrange
            var playCapId = 1;
            Player player = null;
            A.CallTo(() => _playerService.GetPlayerOfPlayCap(playCapId)).Returns(player);
            var controller = new PlayerController(_playerService, _mapper);

            // Act
            var result = await controller.GetPlayCapPlayer(playCapId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("PlayCap for player does not exist");
        }

        [Fact]
        public async Task GetPlayCapPlayer_WithPlayerWithoutFirstName_ReturnsNotFoundResult()
        {
            // Arrange
            var playCapId = 1;
            var player = new Player { FirstName = null };
            A.CallTo(() => _playerService.GetPlayerOfPlayCap(playCapId)).Returns(player);
            var controller = new PlayerController(_playerService, _mapper);

            // Act
            var result = await controller.GetPlayCapPlayer(playCapId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("player of playcap specified not found");
        }

        [Fact]
        public async Task CreatePlayer_WithValidPlayer_ReturnsOkObjectResult()
        {
            // Arrange
            var playerCreate = new Player();
            var createdPlayer = new Player();
            A.CallTo(() => _playerService.GetPlayers()).Returns(new List<Player>());
            A.CallTo(() => _mapper.Map<Player>(playerCreate)).Returns(createdPlayer);
            A.CallTo(() => _playerService.CreatePlayer(createdPlayer)).Returns(createdPlayer);
            var controller = new PlayerController(_playerService, _mapper);

            // Act
            var result = await controller.CreatePlayer(playerCreate);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(createdPlayer);
        }


        [Fact]
        public async Task UpdatePlayer_WithExistingPlayer_ReturnsOkObjectResult()
        {
            // Arrange
            var playerInput = new PlayerDTO();
            var updatedPlayer = new Player();
            A.CallTo(() => _mapper.Map<Player>(playerInput)).Returns(updatedPlayer);
            A.CallTo(() => _playerService.UpdatePlayer(updatedPlayer)).Returns(updatedPlayer);
            var controller = new PlayerController(_playerService, _mapper);

            // Act
            var result = await controller.UpdatePlayer(playerInput);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(_mapper.Map<PlayerDTO>(updatedPlayer));
        }

        [Fact]
        public async Task DeletePlayer_WithExistingPlayer_ReturnsOkObjectResult()
        {
            // Arrange
            var playerId = 1;
            var result = true;
            A.CallTo(() => _playerService.DeletePlayer(playerId)).Returns(result);
            var controller = new PlayerController(_playerService, _mapper);

            // Act
            var actionResult = await controller.DeletePlayer(playerId);

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();
            var okResult = actionResult as OkObjectResult;
            okResult.Value.Should().Be("player successfully deleted");
        }

        [Fact]
        public async Task DeletePlayer_WithNonExistingPlayer_ReturnsBadRequestResult()
        {
            // Arrange
            var playerId = 1;
            var result = false;
            A.CallTo(() => _playerService.DeletePlayer(playerId)).Returns(result);
            var controller = new PlayerController(_playerService, _mapper);

            // Act
            var actionResult = await controller.DeletePlayer(playerId);

            // Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = actionResult as BadRequestObjectResult;
            badRequestResult.Value.Should().Be("Deleting a player failed");
        }
    }
}
