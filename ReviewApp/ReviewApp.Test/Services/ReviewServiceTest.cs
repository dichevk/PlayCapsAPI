using PlayCapsViewer.Services;

namespace ReviewApp.Test.Services
{
    public class ReviewServiceTest
    {
        private DbContextTestSetup _testSetup;
        private ReviewService _reviewService;
        private PlayCapService _playCapService;
        private ReviewerService _reviewerService;
        async void Init()
        {
            _testSetup = new DbContextTestSetup();
            var testContext = await _testSetup.GetDatabaseContext();
            _reviewService = new ReviewService(testContext);
            _playCapService = new PlayCapService(testContext);
            _reviewerService = new ReviewerService(testContext);

        }
        [Fact]
        public async void ReviewService_GetAllReviews_ReturnsListOfReviews()
        {
            //Assert 
            Init();
            //Act 
            var result = await _reviewService.GetAllReviews();
            //Assert
            result.Should().NotBeEmpty();
            result.Should().BeOfType<List<Review>>();
        }
        [Fact]
        public async void ReviewService_GetReviewById_ReturnsReview()
        {
            //Arrange
            Init();
            //Act 
            var result = await _reviewService.GetReviewById(1);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Review>();
        }
        [Fact]
        public async void ReviewService_GetReviewById_WithInvalidReviewId_ReturnsNull()
        {
            //Arrange
            Init();
            //Act 
            var result = await _reviewService.GetReviewById(-100000000);
            //Assert
            result.Should().BeNull();
        }
        [Fact]
        public async void ReviewService_CreateReview_ReturnsReview()
        {
            //Arrange
            Init();
            var testPlayCap = await _playCapService.GetPlayCap(1);
            var testReviewer = await _reviewerService.GetReviewerById(1);
            Review newReview = new Review()
            {
                Id = 100063131,
                Rating = 3,
                PlayCap = testPlayCap,
                Text = "Hey",
                Title = "test",
                Reviewer = testReviewer,

            };
            //Act 
            var result = await _reviewService.CreateReview(newReview);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Review>();
        }
    }
}
