using FakeItEasy;
using PlayCapsViewer.DTO;
using PlayCapsViewer.Models;

namespace ReviewApp.Test.Controllers
{
    public class ReviewControllerTests
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly ReviewController _controller;

        public ReviewControllerTests()
        {
            _reviewService = A.Fake<IReviewService>();
            _mapper = A.Fake<IMapper>();
            _controller = new ReviewController(_reviewService, _mapper);
        }

        [Fact]
        public async Task GetReviews_WhenReviewsExist_ReturnsOkResultWithReviews()
        {
            // Arrange
            var fakeReviews = A.CollectionOfFake<Review>(5).ToList();
            var fakeReviewDTOs = A.CollectionOfFake<ReviewDTO>(5).ToList();

            A.CallTo(() => _reviewService.GetAllReviews()).Returns(fakeReviews);
            A.CallTo(() => _mapper.Map<List<ReviewDTO>>(fakeReviews)).Returns(fakeReviewDTOs);

            // Act
            var result = await _controller.GetReviews();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(fakeReviewDTOs);
        }

        [Fact]
        public async Task GetReviews_WhenNoReviewsExist_ReturnsNotFoundResult()
        {
            // Arrange
            var fakeReviews = new List<Review>();

            A.CallTo(() => _reviewService.GetAllReviews()).Returns(fakeReviews);

            // Act
            var result = await _controller.GetReviews();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("No reviews found");
        }

        [Fact]
        public async Task GetAllReviewsByPlayCap_WhenReviewsExist_ReturnsOkResultWithMatchingReviews()
        {
            // Arrange
            int playCapId = 1;
            var fakeReviews = A.CollectionOfFake<Review>(5).ToList();
            var fakeReviewDTOs = A.CollectionOfFake<ReviewDTO>(5).ToList();

            A.CallTo(() => _reviewService.GetAllReviewsByPlayCap(playCapId)).Returns(fakeReviews);
            A.CallTo(() => _mapper.Map<List<ReviewDTO>>(fakeReviews)).Returns(fakeReviewDTOs);

            // Act
            var result = await _controller.GetAllReviewsByPlayCap(playCapId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(fakeReviewDTOs);
        }

        [Fact]
        public async Task GetAllReviewsByPlayCap_WhenNoReviewsExist_ReturnsNotFoundResult()
        {
            // Arrange
            int playCapId = 1;
            var fakeReviews = new List<Review>();

            A.CallTo(() => _reviewService.GetAllReviewsByPlayCap(playCapId)).Returns(fakeReviews);

            // Act
            var result = await _controller.GetAllReviewsByPlayCap(playCapId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("No reviews found");
        }
        [Fact]
        public async Task GetAllReviewsByReviewer_WhenReviewsExist_ReturnsOkResultWithMatchingReviews()
        {
            // Arrange
            int reviewerId = 1;
            var fakeReviews = A.CollectionOfFake<Review>(5).ToList();
            var fakeReviewDTOs = A.CollectionOfFake<ReviewDTO>(5).ToList();

            A.CallTo(() => _reviewService.GetAllReviewsByReviewer(reviewerId)).Returns(fakeReviews);
            A.CallTo(() => _mapper.Map<List<ReviewDTO>>(fakeReviews)).Returns(fakeReviewDTOs);

            // Act
            var result = await _controller.GetAllReviewsByReviewer(reviewerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(fakeReviewDTOs);
        }

        [Fact]
        public async Task GetAllReviewsByReviewer_WhenNoReviewsExist_ReturnsNotFoundResult()
        {
            // Arrange
            int reviewerId = 1;
            var fakeReviews = new List<Review>();

            A.CallTo(() => _reviewService.GetAllReviewsByReviewer(reviewerId)).Returns(fakeReviews);

            // Act
            var result = await _controller.GetAllReviewsByReviewer(reviewerId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("No reviews found");
        }
        [Fact]
        public async Task GetReview_WhenReviewExists_ReturnsOkResultWithMatchingReview()
        {
            // Arrange
            int reviewId = 1;
            var fakeReview = A.Fake<Review>();
            var fakeReviewDTO = A.Fake<ReviewDTO>();

            A.CallTo(() => _reviewService.GetReviewById(reviewId)).Returns(fakeReview);
            A.CallTo(() => _mapper.Map<ReviewDTO>(fakeReview)).Returns(fakeReviewDTO);

            // Act
            var result = await _controller.GetReview(reviewId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(fakeReviewDTO);
        }

        [Fact]
        public async Task GetReview_WhenReviewDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            int reviewId = 1;
            Review fakeReview = null;

            A.CallTo(() => _reviewService.GetReviewById(reviewId)).Returns(fakeReview);

            // Act
            var result = await _controller.GetReview(reviewId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("No reviews found");
        }
        [Fact]
        public async Task CreateReview_WhenModelStateIsInvalid_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidReviewDTO = A.Fake<ReviewDTO>();
            _controller.ModelState.AddModelError("Rating", "The Rating field is required.");

            // Act
            var result = await _controller.CreateReview(invalidReviewDTO);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeOfType<SerializableError>();
        }
        [Fact]
        public async Task ReviewController_CreateReview_ValidInput_ReturnsOkResponse()
        {
            // Arrange
            var reviewDTO = new ReviewDTO { Id = 1, Title = "Great Play", Text = "The play was amazing!", Rating = 5 };
            var review = new Review { Id = 1, Title = "Great Play", Text = "The play was amazing!", Rating = 5 };
            A.CallTo(() => _mapper.Map<Review>(reviewDTO)).Returns(review);
            A.CallTo(() => _reviewService.CreateReview(review)).Returns(review);
            var controller = new ReviewController(_reviewService, _mapper);

            // Act
            var result = await controller.CreateReview(reviewDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            A.CallTo(() => _reviewService.CreateReview(review)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateReview_WhenModelStateIsInvalid_ReturnsBadRequestResult()
        {
            // Arrange
            var invalidReviewDTO = A.Fake<ReviewDTO>();
            _controller.ModelState.AddModelError("Rating", "The Rating field is required.");

            // Act
            var result = await _controller.UpdateReview(invalidReviewDTO);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Value.Should().BeOfType<SerializableError>();
        }

        [Fact]
        public async Task ReviewController_CreateReview_ReturnsOkResponse()
        {
            // Arrange
            var reviewDTO = new ReviewDTO { Id = 1, Title = "Great Play", Text = "The play was amazing!", Rating = 5 };
            var review = new Review { Id = 1, Title = "Great Play", Text = "The play was amazing!", Rating = 5 };
            A.CallTo(() => _mapper.Map<Review>(reviewDTO)).Returns(review);
            A.CallTo(() => _reviewService.CreateReview(review)).Returns(review);
            var controller = new ReviewController(_reviewService, _mapper);

            // Act
            var result = await controller.CreateReview(reviewDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            A.CallTo(() => _reviewService.CreateReview(review)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateReview_WhenReviewIsUpdatedSuccessfully_ReturnsOkResultWithUpdatedReview()
        {
            // Arrange
            var reviewInput = new ReviewDTO { Id = 1, Title = "Review Title", Text = "Review Text", Rating = 5, Reviewer = new Reviewer(), PlayCap = new PlayCap() };
            var fakeReview = A.Fake<Review>();
            var fakeReviewDTO = A.Fake<ReviewDTO>();

            A.CallTo(() => _mapper.Map<Review>(reviewInput)).Returns(fakeReview);
            A.CallTo(() => _reviewService.UpdateReview(fakeReview)).Returns(fakeReview);
            A.CallTo(() => _mapper.Map<ReviewDTO>(fakeReview)).Returns(fakeReviewDTO);

            // Act
            var result = await _controller.UpdateReview(reviewInput);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(fakeReviewDTO);
        }


        [Fact]
        public async Task ReviewController_DeleteReview_ExistingReview_ReturnsNoContent()
        {
            // Arrange
            int reviewId = 1;
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).Returns(true);
            var controller = new ReviewController(_reviewService, _mapper);

            // Act
            var result = await controller.DeleteReview(reviewId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var noContentResult = result as NoContentResult;
            noContentResult.StatusCode.Should().Be(204);
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ReviewController_DeleteReview_NonExistingReview_ReturnsNotFound()
        {
            // Arrange
            int reviewId = 1;
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).Returns(false);
            var controller = new ReviewController(_reviewService, _mapper);

            // Act
            var result = await controller.DeleteReview(reviewId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().Be("Deleting a review with that id failed");
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ReviewController_DeleteReview_ValidModelState_ReviewNotFound_ReturnsNotFound()
        {
            // Arrange
            int reviewId = 1;
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).Returns(false);
            var controller = new ReviewController(_reviewService, _mapper);

            // Act
            var result = await controller.DeleteReview(reviewId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.StatusCode.Should().Be(404);
            notFoundResult.Value.Should().Be("Deleting a review with that id failed");
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ReviewController_DeleteReview_ValidModelState_ReviewDeleted_ReturnsNoContent()
        {
            // Arrange
            int reviewId = 1;
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).Returns(true);
            var controller = new ReviewController(_reviewService, _mapper);

            // Act
            var result = await controller.DeleteReview(reviewId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var noContentResult = result as NoContentResult;
            noContentResult.StatusCode.Should().Be(204);
            A.CallTo(() => _reviewService.DeleteReview(reviewId)).MustHaveHappenedOnceExactly();
        }

    }
}
