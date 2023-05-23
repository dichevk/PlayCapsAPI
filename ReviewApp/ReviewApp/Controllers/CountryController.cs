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
    [Route("/api/countries")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Countries
        /// </summary>
        /// <remarks>
        /// Get all the countries from the db with mapping to the CategoryDTO
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryService.GetCountries();
            var countriesMap = _mapper.Map<List<CountryDTO>>(countries);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countriesMap);
        }
        /// <summary>
        /// Get a specific country
        /// </summary>
        /// <param name="countryId">The id of the country</param>
        /// <remarks>
        /// Get a specific country by its id 
        /// </remarks>
        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountry(int countryId)
        {
            var country = await _countryService.GetCountry(countryId);
            if (country == null)
            {
                return NotFound("Country was not found");
            }


            var countryObj = _mapper.Map<CountryDTO>(_countryService.GetCountry(countryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countryObj);
        }
        /// <summary>
        /// Get the country for a specific reviewer
        /// <param name="reviewerId">The id of the reviewer</param>
        /// </summary>
        /// <remarks>
        /// returns the country of the reviewer
        /// </remarks>
        [HttpGet("/reviewers/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountryOfReviewer(int reviewerId)
        {
            var country = await _countryService.GetCountryOfReviewer(reviewerId);
            if (country == null)
            {
                return NotFound("Country for that reviewer was not found");
            }
            var countryObj = _mapper.Map<CountryDTO>(
               country);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(countryObj);
        }
        /// <summary>
        /// Get the country for a specific player
        /// <param name="playerId">The id of the player</param>
        /// </summary>
        /// <remarks>
        /// returns the country of the player
        /// </remarks>
        [HttpGet("/players/{playerId}")]
        [ProducesResponseType(200, Type = typeof(CountryDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountryOfPlayer(int playerId)
        {
            var country = await _countryService.GetCountryOfPlayer(playerId);
            if (country == null)
            {
                return NotFound("Country for that player was not found");
            }
            var countryObj = _mapper.Map<CountryDTO>(
               country);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(countryObj);
        }
        /// <summary>
        /// Create a new country
        /// <param name="countryCreate">The countryDTO received</param>
        /// </summary>
        /// <remarks>
        /// returns the newly created country
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateCountry([FromBody] CountryDTO countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);

            List<Country> countries = await _countryService.GetCountries();
            Country country = countries.Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(countryCreate);

            var result = await _countryService.CreateCountry(countryMap);
            if (result == null)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok(countryMap);
        }
        /// <summary>
        /// Update a country
        /// <param name="countryId">The id of the country to be updated</param>
        /// <param name="updatedCountry">The countryDTO that contains the new information</param>
        /// </summary>
        /// <remarks>
        /// returns the upadted country
        /// </remarks>
        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCountry(int countryId, [FromBody] CountryDTO updatedCountry)
        {
            if (updatedCountry == null)
                return BadRequest(ModelState);

            if (countryId != updatedCountry.Id)
                return BadRequest(ModelState);

            Country country = await _countryService.GetCountry(countryId);
            if (country == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var countryMap = _mapper.Map<Country>(updatedCountry);
            var result = await _countryService.UpdateCountry(countryMap);
            if (result == null)
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return Ok(result);
        }
        /// <summary>
        /// Delete a country
        /// <param name="countryId">The id of the country to be deleted</param>
        /// </summary>
        /// <remarks>
        /// returns status 204 if the contry was successfully deleted
        /// </remarks>
        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            var result = await _countryService.DeleteCountry(countryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
                return NotFound();
            }
            //deletion was successful           
            return NoContent();
        }
    }
}
