using PlayCapsViewer.Services;

namespace ReviewApp.Test.Services
{
    public class ReviewerServiceTest
    {
        private DbContextTestSetup _testSetup;
        private ReviewerService _reviewerService;
        async void Init()
        {
            _testSetup = new DbContextTestSetup();
            var testContext = await _testSetup.GetDatabaseContext();
            _reviewerService = new ReviewerService(testContext);
        }
        [Fact]
        public async void ReviewerService_GetAllReviewers_ReturnsListOfReviewers()
        {
            //Assert 
            Init();
            //Act 
            var result = await _reviewerService.GetAllReviewers();
            //Assert
            result.Should().NotBeEmpty();
            result.Should().BeOfType<List<Reviewer>>();
        }
        [Fact]
        public async void ReviewerService_GetReviewerById_ReturnsReviewer()
        {
            //Arrange
            Init();
            //Act 
            var result = await _reviewerService.GetReviewerById(1);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Reviewer>();
        }
        [Fact]
        public async void ReviewerService_GetReviewerById_WithInvalidReviewerId_ReturnsNull()
        {
            //Arrange
            Init();
            //Act 
            var result = await _reviewerService.GetReviewerById(-100000000);
            //Assert
            result.Should().BeNull();
        }
        [Fact]
        public async void ReviewerService__ReturnsNull()
        {
            //Arrange
            Init();
            Reviewer newReviewer = new Reviewer()
            {
                Id = 100063131,
                Country = new Country() { Id = 106801310, Name = "Test1" },
                FirstName = "Sam",
                LastName = "Samuel",
            };
            //Act 
            var result = await _reviewerService.CreateReviewer(newReviewer);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Reviewer>();
        }
    }
}
