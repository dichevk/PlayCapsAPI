using FakeItEasy;

namespace playCapReviewApp.Tests.Controller
{
    public class PlayCapControllerTests
    {
        private readonly IPlayCapService _playCapService;
        private readonly IMapper _mapper;
        public PlayCapControllerTests()
        {
            _playCapService = A.Fake<IPlayCapService>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task PlayCapController_GetPlayCaps_ReturnOK()
        {
            //Arrange
            var playCaps = A.Fake<ICollection<PlayCapDTO>>();
            var playCapList = A.Fake<List<PlayCapDTO>>();
            A.CallTo(() => _mapper.Map<List<PlayCapDTO>>(playCaps)).Returns(playCapList);
            var controller = new PlayCapController(_playCapService, _mapper);

            //Act
            var result = await controller.GetAllPlayCaps();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }



        [Fact]
        public async Task PlayCapController_GetAllPlayCaps_WithPlayCaps_ReturnsOkObjectResult()
        {
            // Arrange
            var playCaps = new List<PlayCap>();
            var playCapDTOs = new List<PlayCapDTO>();
            A.CallTo(() => _playCapService.GetAllPlayCaps()).Returns(playCaps);
            A.CallTo(() => _mapper.Map<List<PlayCapDTO>>(playCaps)).Returns(playCapDTOs);
            var controller = new PlayCapController(_playCapService, _mapper);

            // Act
            var result = await controller.GetAllPlayCaps();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
        }



        [Fact]
        public async Task PlayCapController_GetPlayCapById_WithValidPlayCapId_ReturnsOkObjectResult()
        {
            // Arrange
            int playCapId = 1;
            var playCap = A.Fake<PlayCap>();
            var playCapDTO = A.Fake<PlayCapDTO>();
            A.CallTo(() => _playCapService.GetPlayCap(playCapId)).Returns(playCap);
            A.CallTo(() => _mapper.Map<PlayCapDTO>(playCap)).Returns(playCapDTO);
            var controller = new PlayCapController(_playCapService, _mapper);

            // Act
            var result = await controller.GetPlayCapById(playCapId);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(playCapDTO);
        }

        [Fact]
        public async Task PlayCapController_GetPlayCapByName_WithValidPlayCapName_ReturnsOkObjectResult()
        {
            // Arrange
            string playCapName = "Test PlayCap";
            var playCap = A.Fake<PlayCap>();
            var playCapDTO = A.Fake<PlayCapDTO>();
            A.CallTo(() => _playCapService.GetPlayCapByName(playCapName)).Returns(playCap);
            A.CallTo(() => _mapper.Map<PlayCapDTO>(playCap)).Returns(playCapDTO);
            var controller = new PlayCapController(_playCapService, _mapper);

            // Act
            var result = await controller.GetPlayCapByName(playCapName);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(playCapDTO);
        }

        [Fact]
        public async Task PlayCapController_GetPlayCapRating_WithValidPlayCapId_ReturnsOkObjectResult()
        {
            // Arrange
            int playCapId = 1;
            decimal rating = 4.5m;
            A.CallTo(() => _playCapService.GetPlayCapRating(playCapId)).Returns(rating);
            var controller = new PlayCapController(_playCapService, _mapper);

            // Act
            var result = await controller.GetPlayCapRating(playCapId);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(rating);
        }



        [Fact]
        public async Task PlayCapController_UpdatePlayCap_WithValidData_ReturnsOkObjectResult()
        {
            // Arrange
            var playCapDTO = A.Fake<PlayCapDTO>();
            var playCap = A.Fake<PlayCap>();
            A.CallTo(() => _mapper.Map<PlayCap>(playCapDTO)).Returns(playCap);
            A.CallTo(() => _playCapService.GetPlayCap(playCapDTO.Id)).Returns(playCap);
            A.CallTo(() => _playCapService.UpdatePlayCap(playCap)).Returns(playCap);
            var controller = new PlayCapController(_playCapService, _mapper);

            // Act
            var result = await controller.UpdatePlayCap(playCapDTO);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(playCapDTO);
        }
        [Fact]
        public async Task PlayCapController_CreatePlayCap_ReturnOK()
        {
            //Arrange
            int playerId = 1;
            int catId = 2;
            var playCap = A.Fake<PlayCap>();
            var playCapDTO = A.Fake<PlayCapDTO>();
            var playCaps = A.Fake<ICollection<PlayCapDTO>>();
            var playCapList = A.Fake<IList<PlayCapDTO>>();


            A.CallTo(() => _mapper.Map<PlayCap>(playCapDTO)).Returns(playCap);
            A.CallTo(() => _playCapService.CreatePlayCap(playCap, playerId, catId));
            var controller = new PlayCapController(_playCapService, _mapper);

            //Act
            var result = await controller.CreatePlayCap(playCapDTO, playerId, catId);

            //Assert
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task PlayCapController_CreatePlayCap_ReturnNotOk()
        {
            //Arrange
            int playerId = 1;
            int catId = 2;
            var playCap = A.Fake<PlayCap>();
            var playCapDTO = A.Fake<PlayCapDTO>();
            var playCaps = A.Fake<ICollection<PlayCapDTO>>();
            var playCapList = A.Fake<IList<PlayCapDTO>>();

            playCapDTO.Id = -999;//invalid Id
            A.CallTo(() => _mapper.Map<PlayCap>(playCapDTO)).Returns(playCap);
            A.CallTo(() => _playCapService.CreatePlayCap(playCap, playerId, catId));
            var controller = new PlayCapController(_playCapService, _mapper);

            //Act
            var result = await controller.CreatePlayCap(playCapDTO, playerId, catId);

            //Assert
            result.Should().NotBeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task PlayCapController_DeletePlayCap_WithValidPlayCapId_ReturnsOkResult()
        {
            // Arrange
            int playCapId = 1;
            int playerId = 1;
            int catId = 1;
            var playCap = A.Fake<PlayCap>();
            A.CallTo(() => _playCapService.CreatePlayCap(playCap, playerId, catId)).Returns(
                playCap);
            A.CallTo(() => _playCapService.GetPlayCap(playCapId)).Returns(playCap);
            A.CallTo(() => _playCapService.DeletePlayCap(playCapId)).Returns(true);
            var controller = new PlayCapController(_playCapService, _mapper);

            // Act
            var result = await controller.DeletePlayCap(playCapId);

            // Assert
            result.Should().NotBeNull().And.BeOfType<NoContentResult>();
        }
    }
}