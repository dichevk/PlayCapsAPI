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
    [Route("/api/reviewers")]
    public class ReviewerController : Controller
    {
        private readonly IReviewerService _reviewerService;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerService reviewerService, IMapper mapper)
        {
            _reviewerService = reviewerService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all the reviewers
        /// </summary>
        /// <returns>a list of the reviewers with mapping to the reviewerDTO</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ReviewDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewers()
        {
            var result = await _reviewerService.GetAllReviewers();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_mapper.Map<List<ReviewerDTO>>(result));
        }
        /// <summary>
        /// Get the detailed information about a specific reviwer
        /// </summary>
        /// <param name="reviewerId">the id of the reviewer we want to fetch</param>
        /// <returns>Returns ReviewerDTO object if found</returns>
        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReviewer(int reviewerId)
        {
            var result = await _reviewerService.GetReviewerById(reviewerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (result == null)
            {
                return NotFound("No reviewer with that Id was found");
            }
            return Ok(_mapper.Map<ReviewerDTO>(result));
        }
        /// <summary>
        /// Get the detailed information about a specific reviwer
        /// </summary>
        /// <param name="reviewerName">the name of the reviewer we want to fetch</param>
        /// <returns>Returns ReviewerDTO object if reviewer exists</returns>
        [HttpGet("{reviewerName}")]
        [ProducesResponseType(200, Type = typeof(ReviewDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReviewerByName(string reviewerName)
        {
            var result = await _reviewerService.GetReviewerByName(reviewerName);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (result == null)
            {
                return NotFound("No reviewer with that name was found");
            }
            return Ok(_mapper.Map<ReviewerDTO>(result));
        }
        /// <summary>
        /// Create a new Reviewer
        /// </summary>
        /// <param name="reviewerInput">The reviewerDTO sent with the body of the reuqest</param>
        /// <returns>the newly created reviewerDTO object</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateReviewer([FromBody] ReviewerDTO reviewerInput)
        {
            var reviewerMap = _mapper.Map<Reviewer>(reviewerInput);
            var result = await _reviewerService.CreateReviewer(reviewerMap);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (result == null)
            {
                UnprocessableEntity("Reviewer with that Id or name already exists!");
            }
            return Ok(_mapper.Map<ReviewerDTO>(result));
        }
        /// <summary>
        /// Update an existing reviewer record 
        /// </summary>
        /// <param name="reviewerId">the id of the reviewer we would like to update</param>
        /// <param name="reviewerInput">The data sent from the frontend</param>
        /// <returns>returns the updated reviewerData </returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ReviewerDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateReviewer(int reviewerId, [FromBody] ReviewerDTO reviewerInput)
        {
            var reviewerMap = _mapper.Map<Reviewer>(reviewerInput);
            var result = await _reviewerService.UpdateReviewer(reviewerId, reviewerMap);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (result == null)
            {
                return NotFound("The entity you are trying to update does not exist!");
            }
            return Ok(_mapper.Map<ReviewerDTO>(result));
        }
        /// <summary>
        /// Delete a reviewer 
        /// </summary>
        /// <param name="reviewerId">the id of the reviewer we would like to delete</param>
        /// <returns>returns 204 if the deletion was successful, 4xx response otherwise</returns>
        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteReviewer(int reviewerId)
        {
            bool deleted = await _reviewerService.DeleteReviewer(reviewerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!deleted)
            {
                return NotFound("The entity you are trying to delete was not found!");
            }
            return NoContent();
        }
    }
}
