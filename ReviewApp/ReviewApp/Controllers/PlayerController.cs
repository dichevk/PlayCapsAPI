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
    [Route("/api/players")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get All Players
        /// </summary>
        /// <remarks>
        /// Get all the players from the db with mapping to the PlayerDTO
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PlayerDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _playerService.GetPlayers();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (players == null)
            {
                return NotFound("no players found");
            }
            return Ok(_mapper.Map<List<PlayerDTO>>(players));
        }
        /// <summary>
        /// Get the playcap's player
        /// </summary>
        /// <param name="playCapId">The id of the playcap</param>
        /// <remarks>
        /// returns the playerDTO of the playcap
        /// </remarks>
        [HttpGet("{playCapId}/player")]
        [ProducesResponseType(200, Type = typeof(PlayerDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlayCapPlayer(int playCapId)
        {
            var player = await _playerService.GetPlayerOfPlayCap(playCapId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (player == null)
            {
                return NotFound("PlayCap for player does not exist");
            }
            if (player.FirstName == null)
            {
                return NotFound("player of playcap specified not found");
            }

            var playerMap = _mapper.Map<PlayerDTO>(player);

            return Ok(playerMap);
        }
        /// <summary>
        /// Get a specific player
        /// </summary>
        /// <param name="playerId">The id of the player</param>
        /// <remarks>
        /// Returns the specific player
        /// </remarks>
        [HttpGet("{playerId}")]
        [ProducesResponseType(200, Type = typeof(PlayerDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPlayer(int playerId)
        {
            var player = await _playerService.GetPlayer(playerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (player == null)
            {
                return NotFound("Player was not found");
            }
            var playerObj = _mapper.Map<PlayerDTO>(player);
            return Ok(playerObj);
        }
        /// <summary>
        /// Create a new player
        /// <param name="playerCreate">The playerDTO received</param>
        /// </summary>
        /// <remarks>
        /// returns the newly created player
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Player))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> Createplayer([FromBody] Player playerCreate)
        {
            if (playerCreate == null)
                return BadRequest(ModelState);

            List<Player> players = await _playerService.GetPlayers();
            Player player = players.Where(c => c.FirstName.Trim().ToUpper().Concat(c.LastName.Trim().ToUpper()) == playerCreate.FirstName.TrimEnd().ToUpper().Concat(playerCreate.LastName.Trim().ToUpper())).FirstOrDefault();
            if (player != null)
            {
                ModelState.AddModelError("", "player already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var playerMap = _mapper.Map<Player>(playerCreate);

            var result = await _playerService.CreatePlayer(playerMap);
            if (result == null)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok(playerMap);
        }
        /// <summary>
        /// Update an existing player
        /// <param name="playerInput">The playerDTO received</param>
        /// </summary>
        /// <remarks>
        /// returns the updated player
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(PlayerDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePlayer([FromBody] PlayerDTO playerInput)
        {
            var playerMap = _mapper.Map<Player>(playerInput);
            var updatePlayer = await _playerService.UpdatePlayer(playerMap);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (updatePlayer != null)
            {
                return Ok(_mapper.Map<PlayerDTO>(updatePlayer));
            }
            return NotFound("Updating a player failed, please check that the correct id has been sent");
        }
        /// <summary>
        /// Delete an existing player
        /// <param name="playerId">The Id of the player received</param>
        /// </summary>
        /// <remarks>
        /// Deletes the player from the db and returns Ok if suceessful
        /// </remarks>
        [HttpDelete("{playerId}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeletePlayer(int playerId)
        {
            var result = await _playerService.DeletePlayer(playerId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (result)
            {
                return Ok("player successfully deleted");
            }
            return BadRequest("Deleting a player failed");
        }
    }
}
