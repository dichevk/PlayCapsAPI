namespace playCapReviewApp.Tests.Controller
{
    public class playCapControllerTests
    {
        private readonly IPlayCapService _playCapService;
        private readonly IMapper _mapper;
        public playCapControllerTests()
        {
            _playCapService = A.Fake<IPlayCapService>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task playCapController_GetPlayCaps_ReturnOK()
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
        public async Task playCapController_CreatePlayCap_ReturnOK()
        {
            //Arrange
            int playerId = 1;
            int catId = 2;
            var playCapMap = A.Fake<PlayCap>();
            var playCap = A.Fake<PlayCap>();
            var playCapCreate = A.Fake<PlayCapDTO>();
            var playCaps = A.Fake<ICollection<PlayCapDTO>>();
            var playCapList = A.Fake<IList<PlayCapDTO>>();
            A.CallTo(() => _mapper.Map<PlayCap>(playCapCreate)).Returns(playCap);
            var controller = new PlayCapController(_playCapService, _mapper);

            //Act
            var result = await controller.CreatePlayCap(playCapCreate, playerId, catId);

            //Assert
            result.Should().NotBeNull();
            //result.Should().BeAssignableTo<OkObjectResult>();
        }
    }
}