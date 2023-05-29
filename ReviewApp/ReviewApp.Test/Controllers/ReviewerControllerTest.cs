using Microsoft.AspNetCore.Http;

namespace ReviewApp.Test.Controllers
{
    public class ReviewerControllerTest
    {
        private readonly IReviewerService _reviewerService;
        private readonly IMapper _mapper;

        public ReviewerControllerTest()
        {
            _reviewerService = A.Fake<IReviewerService>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task ReviewerController_CreateReviewer_ReturnOk()
        {
            // Arrange
            var reviewerDTO = A.Fake<ReviewerDTO>();
            var reviewer = A.Fake<Reviewer>();
            A.CallTo(() => _mapper.Map<Reviewer>(reviewerDTO)).Returns(reviewer);
            A.CallTo(() => _reviewerService.CreateReviewer(reviewer)).Returns(Task.FromResult(reviewer));
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.CreateReviewer(reviewerDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
        }

        [Fact]
        public async Task ReviewerController_CreateReviewer_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var reviewerDTO = A.Fake<ReviewerDTO>();
            var controller = new ReviewerController(_reviewerService, _mapper);
            controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");

            // Act
            var result = await controller.CreateReviewer(reviewerDTO);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        // GetReviewers_ReturnOk
        [Fact]
        public async Task ReviewerController_GetReviewers_ReturnOk()
        {
            // Arrange
            var reviewers = new List<Reviewer>(); // Populate with sample data
            A.CallTo(() => _reviewerService.GetAllReviewers()).Returns(reviewers);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.GetReviewers();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        // GetReviewers_InvalidModel_ReturnsBadRequest
        [Fact]
        public async Task ReviewerController_GetReviewers_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var controller = new ReviewerController(_reviewerService, _mapper);
            controller.ModelState.AddModelError("error", "Invalid model");

            // Act
            var result = await controller.GetReviewers();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        // GetReviewer_ValidReviewerId_ReturnOk
        [Fact]
        public async Task ReviewerController_GetReviewer_ValidReviewerId_ReturnOk()
        {
            // Arrange
            int reviewerId = 1; // Provide a valid reviewer ID
            var reviewer = new Reviewer(); // Populate with sample data
            A.CallTo(() => _reviewerService.GetReviewerById(reviewerId)).Returns(reviewer);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.GetReviewer(reviewerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        // GetReviewer_InvalidModel_ReturnsBadRequest
        [Fact]
        public async Task ReviewerController_GetReviewer_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var controller = new ReviewerController(_reviewerService, _mapper);
            controller.ModelState.AddModelError("error", "Invalid model");

            // Act
            var result = await controller.GetReviewer(1);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        // GetReviewer_InvalidReviewerId_ReturnsNotFound
        [Fact]
        public async Task ReviewerController_GetReviewer_InvalidReviewerId_ReturnsNotFound()
        {
            // Arrange
            int reviewerId = 1; // Provide an invalid reviewer ID
            Reviewer reviewer = null;
            A.CallTo(() => _reviewerService.GetReviewerById(reviewerId)).Returns(reviewer);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.GetReviewer(reviewerId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        // GetReviewerByName_ValidReviewerName_ReturnOk
        [Fact]
        public async Task ReviewerController_GetReviewerByName_ValidReviewerName_ReturnOk()
        {
            // Arrange
            string reviewerName = "John Doe"; // Provide a valid reviewer name
            var reviewer = new Reviewer(); // Populate with sample data
            A.CallTo(() => _reviewerService.GetReviewerByName(reviewerName)).Returns(reviewer);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.GetReviewerByName(reviewerName);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        // GetReviewerByName_InvalidModel_ReturnsBadRequest
        [Fact]
        public async Task ReviewerController_GetReviewerByName_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var controller = new ReviewerController(_reviewerService, _mapper);
            controller.ModelState.AddModelError("error", "Invalid model");

            // Act
            var result = await controller.GetReviewerByName("John Doe");

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        // GetReviewerByName_InvalidReviewerName_ReturnsNotFound
        [Fact]
        public async Task ReviewerController_GetReviewerByName_InvalidReviewerName_ReturnsNotFound()
        {
            // Arrange
            string reviewerName = "John Doe"; // Provide an invalid reviewer name
            Reviewer reviewer = null;
            A.CallTo(() => _reviewerService.GetReviewerByName(reviewerName)).Returns(reviewer);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.GetReviewerByName(reviewerName);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        // CreateReviewer_ValidReviewer_ReturnOk
        [Fact]
        public async Task ReviewerController_CreateReviewer_ValidReviewer_ReturnOk()
        {
            // Arrange
            var reviewerInput = new ReviewerDTO(); // Provide a valid reviewer input
            var reviewer = new Reviewer(); // Populate with sample data
            A.CallTo(() => _reviewerService.CreateReviewer(A<Reviewer>.Ignored)).Returns(reviewer);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.CreateReviewer(reviewerInput);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        // UpdateReviewer_ValidReviewerIdAndReviewer_ReturnOk
        [Fact]
        public async Task ReviewerController_UpdateReviewer_ValidReviewerIdAndReviewer_ReturnOk()
        {
            // Arrange
            int reviewerId = 1; // Provide a valid reviewer ID
            var reviewerInput = new ReviewerDTO(); // Provide a valid reviewer input
            var reviewer = new Reviewer(); // Populate with sample data
            A.CallTo(() => _reviewerService.UpdateReviewer(reviewerId, A<Reviewer>.Ignored)).Returns(reviewer);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.UpdateReviewer(reviewerId, reviewerInput);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        // UpdateReviewer_InvalidModel_ReturnsBadRequest
        [Fact]
        public async Task ReviewerController_UpdateReviewer_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var controller = new ReviewerController(_reviewerService, _mapper);
            controller.ModelState.AddModelError("error", "Invalid model");

            // Act
            var result = await controller.UpdateReviewer(1, new ReviewerDTO());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        // UpdateReviewer_InvalidReviewerId_ReturnsNotFound
        [Fact]
        public async Task ReviewerController_UpdateReviewer_InvalidReviewerId_ReturnsNotFound()
        {
            // Arrange
            int reviewerId = 1; // Provide an invalid reviewer ID
            Reviewer reviewer = null;
            A.CallTo(() => _reviewerService.UpdateReviewer(reviewerId, A<Reviewer>.Ignored)).Returns(reviewer);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.UpdateReviewer(reviewerId, new ReviewerDTO());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        // DeleteReviewer_ValidReviewerId_ReturnsNoContent
        [Fact]
        public async Task ReviewerController_DeleteReviewer_ValidReviewerId_ReturnsNoContent()
        {
            // Arrange
            int reviewerId = 1; // Provide a valid reviewer ID
            A.CallTo(() => _reviewerService.DeleteReviewer(reviewerId)).Returns(true);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.DeleteReviewer(reviewerId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var noContentResult = result as NoContentResult;
            noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        // DeleteReviewer_InvalidModel_ReturnsBadRequest
        [Fact]
        public async Task ReviewerController_DeleteReviewer_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var controller = new ReviewerController(_reviewerService, _mapper);
            controller.ModelState.AddModelError("error", "Invalid model");

            // Act
            var result = await controller.DeleteReviewer(1);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        // DeleteReviewer_InvalidReviewerId_ReturnsNotFound
        [Fact]
        public async Task ReviewerController_DeleteReviewer_InvalidReviewerId_ReturnsNotFound()
        {
            // Arrange
            int reviewerId = 1; // Provide an invalid reviewer ID
            A.CallTo(() => _reviewerService.DeleteReviewer(reviewerId)).Returns(false);
            var controller = new ReviewerController(_reviewerService, _mapper);

            // Act
            var result = await controller.DeleteReviewer(reviewerId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

    }
}
