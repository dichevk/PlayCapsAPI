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
    [Route("/api/playcaps")]
    public class PlayCapController : Controller
    {
        private readonly IPlayCapService _playCapService;
        private readonly IMapper _mapper;

        public PlayCapController(IPlayCapService playCapService, IMapper mapper)
        {
            _playCapService = playCapService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get All PlayCaps
        /// </summary>
        /// <remarks>
        /// Get all playcaps from the db with mapping to the PlayCapDTO
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PlayCapDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllPlayCaps()
        {
            var result = await _playCapService.GetAllPlayCaps();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultMap = _mapper.Map<List<PlayCapDTO>>(result);

            return Ok(resultMap);
        }
        /// <summary>
        /// Get all playcaps for a category
        /// </summary>
        /// <param name="categoryId">The id of the playCap category from which we want to fetch</param>
        /// <remarks>
        /// returns a list of the playcaps for that category
        /// </remarks>
        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(List<PlayCapDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlayCapsByCategory(int categoryId)
        {
            var playCaps = await _playCapService.GetPlayCapsByCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (playCaps == null)
            {
                return NotFound("Category not found");
            }
            if (playCaps.Count == 0)
            {
                return BadRequest("No playcaps exist for the category specified");
            }
            return Ok(_mapper.Map<List<PlayCapDTO>>(playCaps));
        }
        /// <summary>
        /// Get all playcaps for a specific player
        /// </summary>
        /// <param name="playerId">The id of the player for whom we want to fetch</param>
        /// <remarks>
        /// returns a list of the playcaps for that player
        /// </remarks>
        [HttpGet("{playerId}")]
        [ProducesResponseType(200, Type = typeof(List<PlayCapDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPlayCapsForPlayer(int playerId)
        {
            var playCaps = await _playCapService.GetPlayCapsForPlayer(playerId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (playCaps == null)
            {
                return NotFound("Player not found");
            }
            if (playCaps.Count == 0)
            {
                return BadRequest("No playcaps exist for the player specified");
            }
            return Ok(_mapper.Map<List<PlayCapDTO>>(playCaps));
        }
        /// <summary>
        /// Get a specific playCap
        /// </summary>
        /// <param name="playCapId">The id of the playCap to fetch</param>
        /// <remarks>
        /// Get the specific playcap from the db with mapping to the PlayCapDTO
        /// </remarks>
        [HttpGet("{playCapId}")]
        [ProducesResponseType(200, Type = typeof(PlayCapDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlayCapById(int playCapId)
        {
            var result = await _playCapService.GetPlayCap(playCapId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (result == null)
            {
                return NotFound("PlayCap was not found, please check the id");
            }
            var resultMap = _mapper.Map<PlayCapDTO>(result);

            return Ok(resultMap);
        }
        /// <summary>
        /// Get a specific playCap by it's name
        /// </summary>
        /// <param name="playCapName">The name of the playCap to fetch</param>
        /// <remarks>
        /// Get the specific playcap from the db with mapping to the PlayCapDTO
        /// </remarks>
        [HttpGet("{playCapName}")]
        [ProducesResponseType(200, Type = typeof(PlayCapDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlayCapByName(string playCapName)
        {
            var result = await _playCapService.GetPlayCapByName(playCapName);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (result == null)
            {
                return NotFound("PlayCap was not found, please check that you provided the correct name");
            }
            var resultMap = _mapper.Map<PlayCapDTO>(result);

            return Ok(resultMap);
        }
        /// <summary>
        /// Get a specific playCap's rating
        /// </summary>
        /// <param name="playCapId">The id of the playCap to fetch</param>
        /// <remarks>
        /// returns the rating of that playcap
        /// </remarks>
        [HttpGet("{playCapId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlayCapRating(int playCapId)
        {
            var result = await _playCapService.GetPlayCapRating(playCapId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result);
        }

        /// <summary>
        /// Create a new playcap
        /// </summary>
        /// <param name="categoryId">The id of the category of the new playCap</param>
        /// <param name="playerId">The id of the player associated with the new playCap</param>
        /// <param name="playCapInput">The DTO of the playcap coming from the request</param>
        /// <remarks>
        /// Create a new playcap and return the playcap's DTO if succcessful
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PlayCap))] //display more information about the playCap than just the DTO
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreatePlayCap([FromBody] PlayCapDTO playCapInput, [FromQuery] int playerId, [FromQuery] int categoryId)
        {
            var playCapMap = _mapper.Map<PlayCap>(playCapInput);
            var playCapExists = await _playCapService.GetPlayCap(playCapInput.Id);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (playCapExists != null)
            {

                ModelState.AddModelError("", "Playcap already exists");
                return StatusCode(422, ModelState);

            }
            var result = await _playCapService.CreatePlayCap(playCapMap, playerId, categoryId);

            if (result == null)
            {
                return NotFound("Creating a playcap failed, please check that the ids and the DTO on the payload are correct");
            }
            return Ok(_mapper.Map<PlayCapDTO>(result));
        }
        /// <summary>
        /// Create a an existing playcap
        /// </summary>
        /// <param name="playCapInput">The DTO of the playcap coming from the request</param>
        /// <remarks>
        /// Update an existing playcap and return the playcap's DTO if succcessful
        /// </remarks>
        [HttpPut("{playCapId}")]
        [ProducesResponseType(200, Type = typeof(PlayCap))] //display more information about the playCap than just the DTO
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> UpdatePlayCap([FromBody] PlayCapDTO playCapInput)
        {
            var playCapMap = _mapper.Map<PlayCap>(playCapInput);
            var playCapExists = await _playCapService.GetPlayCap(playCapInput.Id);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (playCapExists == null)
            {
                ModelState.AddModelError("", "Playcap does not exist");
                return StatusCode(422, ModelState);
            }
            var result = await _playCapService.UpdatePlayCap(playCapMap);
            if (result == null)
            {
                return NotFound("Updating a playcap failed, please check that the ids and the DTO on the payload are correct");
            }
            return Ok(_mapper.Map<PlayCapDTO>(result));
        }
        /// <summary>
        /// Delete a playcap
        /// <param name="playCapId">The id of the playCap to be deleted</param>
        /// </summary>
        /// <remarks>
        /// returns status 204 if the playCap was successfully deleted
        /// </remarks>
        [HttpDelete("{playCapId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> DeletePlayCap(int playCapId)
        {
            var playCapExists = await _playCapService.GetPlayCap(playCapId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (playCapExists == null)
            {
                ModelState.AddModelError("", "Playcap does not exist");
                return StatusCode(422, ModelState);
            }
            var result = await _playCapService.DeletePlayCap(playCapId);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong deleting the playcap");
                return NotFound();
            }
            //deletion was successful           
            return NoContent();
        }
    }
}
