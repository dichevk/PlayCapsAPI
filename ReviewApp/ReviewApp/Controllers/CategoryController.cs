using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayCapsViewer.DTO;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;
using PlayCapsViewer.Services;

namespace PlayCapsViewer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <remarks>
        /// Get all the categories with mapping to the CategoryDTO
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetCategories();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
        }
        /// <summary>
        /// Get a single category
        /// </summary>
        /// <param name="categoryId">the id of the category</param>
        /// <remarks>
        /// Get a single category by categoryId with result mapping to the CategoryDTO
        /// </remarks>
        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _categoryService.GetCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (category == null)
                return NotFound("Category was not found");
            return Ok(_mapper.Map<CategoryDTO>(category));
        }
        /// <summary>
        /// Get categories for a playCap
        /// </summary>
        /// <param name="playCapId">the id of the play cap for which we want the categories</param>
        /// <remarks>
        /// Get categories for a playCap with result mapping to the CategoryDTO
        /// </remarks>
        [HttpGet("playCap/{playCapId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetCategoriesByPlayCap(int playCapId)
        {
            var categories = await _categoryService.GetCategoriesByPlayCapId(playCapId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!categories.Any())
                return NotFound("Id of the playcap was not found in correlation with the Category");
            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
        }
        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="category">the category object to be created</param>
        /// <remarks>
        /// Create a category using the the category model
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO category)
        {
            var categoryMap = _mapper.Map<Category>(category);
            var updatedCategory = await _categoryService.CreateCategory(categoryMap);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (updatedCategory != null)
            {
                return Ok(_mapper.Map<CategoryDTO>(updatedCategory));
            }
            return NotFound("Could not create the category with the information provided");
        }
        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="categoryId">the id of the category</param>
        /// <remarks>
        /// Delete a category using the id of the category 
        /// </remarks>
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var response = await _categoryService.DeleteCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!response)
            {
                return NotFound("Category was not found");
            }
            return Ok(true);
        }
        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="category">the category object to be updated</param>
        /// <remarks>
        /// Update a category using the category object 
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO category)
        {
            var categoryMap = _mapper.Map<Category>(category);
            var updatedCategory = await _categoryService.UpdateCategory(categoryMap);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (updatedCategory != null)
            {
                return Ok(_mapper.Map<CategoryDTO>(updatedCategory));
            }
            return NotFound("Category to be updated was not found");
        }
    }
}
