using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayCapsViewer.Interfaces;
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

    }
}
