using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayCapsViewer.DTO;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;
using PlayCapsViewer.Services;

namespace PlayCapsViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
           var categories = _categoryService.GetCategories();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
        }
    }
}
