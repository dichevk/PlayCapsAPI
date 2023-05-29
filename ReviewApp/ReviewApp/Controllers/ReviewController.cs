using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayCapsViewer.DTO;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;

namespace PlayCapsViewer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/reviews")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all the reviews from the db
        /// </summary>
        /// <returns>
        /// Returns all the reviews with mapping to the ReviewDTO
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ReviewDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReviews()
        {
            var result = await _reviewService.GetAllReviews();
            if (result.Count == 0)
            {
                return NotFound("No reviews found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_mapper.Map<List<ReviewDTO>>(result));
        }
        /// <summary>
        /// Get all the reviews for a given playcap from the db
        /// </summary>
        /// <param name="playCapId">the id of the playcap</param>
        /// <returns>
        /// Returns all the matching reviews with mapping to the ReviewDTO
        /// </returns>
        [HttpGet("{playCapId}/reviews")]
        [ProducesResponseType(200, Type = typeof(List<ReviewDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllReviewsByPlayCap(int playCapId)
        {
            var results = await _reviewService.GetAllReviewsByPlayCap(playCapId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (results.Count == 0 || results == null)
            {
                return NotFound("No reviews found");
            }
            return Ok(_mapper.Map<List<ReviewDTO>>(results));
        }
        /// <summary>
        /// Get all the reviews for a given reviewer from the db
        /// </summary>
        /// <param name="reviewerId">the id of the reviewer</param>
        /// <returns>
        /// Returns all the matching reviews with mapping to the ReviewDTO
        /// </returns>
        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(List<ReviewDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllReviewsByReviewer(int reviewerId)
        {
            var results = await _reviewService.GetAllReviewsByReviewer(reviewerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (results.Count == 0 || results == null)
            {
                return NotFound("No reviews found");
            }
            return Ok(_mapper.Map<List<ReviewDTO>>(results));
        }
        /// <summary>
        /// Get a specific review
        /// </summary>
        /// <param name="reviewId">the id of the review</param>
        /// <returns>
        /// Returns the matching reviewDTO if it exists
        /// </returns>
        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReview(int reviewId)
        {
            var result = await _reviewService.GetReviewById(reviewId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (result == null)
            {
                return NotFound("No reviews found");
            }
            return Ok(_mapper.Map<ReviewDTO>(result));
        }
        /// <summary>
        /// Get a specific review for a specific reviewer by their name
        /// </summary>
        /// <param name="reviewerName">the string representing the name of the reviewer it should follow FirstName " " LastName</param>
        /// <returns>
        /// Returns the matching reviewDTO if it exists
        /// </returns>
        [HttpGet("{reviewerName}/review")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReviewByReviewerName(string reviewerName)
        {
            var result = await _reviewService.GetReviewByName(reviewerName);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (result == null)
            {
                return NotFound("No reviews found");
            }
            return Ok(_mapper.Map<ReviewDTO>(result));
        }
        /// <summary>
        /// Get a specific review for a specific reviewer by their id
        /// </summary>
        /// <param name="reviewerId">the id of the reviewer for whom we want the reviews</param>
        /// <param name="reviewId">the id of the review
        /// <returns>
        /// Returns the matching reviewDTO if it exists
        /// </returns>
        [HttpGet("{reviewId}/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReviewByReviewerName(int reviewerId, int reviewId)
        {
            var result = await _reviewService.GetReviewByReviewerId(reviewerId, reviewId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (result == null)
            {
                return NotFound("No reviews found");
            }
            return Ok(_mapper.Map<ReviewDTO>(result));
        }
        /// <summary>
        /// Create a new review 
        /// </summary>
        /// <param name="reviewInput">the input DTO for the review to be created
        /// <returns>the newly created review if successful and if it does not exist already, 4xx otherwise</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDTO reviewInput)
        {
            var reviewMap = _mapper.Map<Review>(reviewInput);
            var result = await _reviewService.CreateReview(reviewMap);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (result == null)
            {
                return UnprocessableEntity();
            }
            return Ok(_mapper.Map<ReviewDTO>(result));
        }
        /// <summary>
        /// Update an existing review 
        /// </summary>
        /// <param name="reviewInput">the input DTO for the review to be created
        /// <returns>the newly updated review if successful and if it exists already, 4xx otherwise</returns>
        [HttpPut("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateReview([FromBody] ReviewDTO reviewInput)
        {
            var reviewMap = _mapper.Map<Review>(reviewInput);
            var result = await _reviewService.UpdateReview(reviewMap);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (result == null)
            {
                return NotFound("Updating a review failed, please check that the entry you are trying to update exists");
            }
            return Ok(_mapper.Map<ReviewDTO>(result));
        }
        /// <summary>
        /// Delete a review 
        /// </summary>
        /// <param name="reviewId">the id of the review to be deleted</param>
        /// <returns>status 204 if the deletion of the review was successful, 4xx otherwise</returns>
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var result = await _reviewService.DeleteReview(reviewId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!result)
            {
                return NotFound("Deleting a review with that id failed");
            }
            return NoContent();
        }
    }
}
