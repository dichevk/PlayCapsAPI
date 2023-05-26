using Microsoft.AspNetCore.Mvc;
using PlayCapsViewer.Controllers;
using PlayCapsViewer.DTO;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Models;


namespace PlayCapsViewer.Tests.Controllers
{
    public class CategoryControllerTests
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly CategoryController _categoryController;

        public CategoryControllerTests()
        {
            _categoryService = A.Fake<ICategoryService>();
            _mapper = A.Fake<IMapper>();
            _categoryController = new CategoryController(_categoryService, _mapper);
        }

        [Fact]
        public void GetCategories_ReturnsAllCategories()
        {
            var categories = A.Fake<ICollection<CategoryDTO>>();
            var categoriesList = A.Fake<List<CategoryDTO>>();
            // Arrange
            var categories2 = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" },
                new Category { Id = 3, Name = "Category 3" }
            };
            var categoryDTOs = categories.Select(c => new CategoryDTO { Id = c.Id, Name = c.Name });
            A.CallTo(() => _mapper.Map<List<CategoryDTO>>(categories)).Returns(categoriesList);

            // Act
            var result = _categoryController.GetCategories();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(categoryDTOs);
        }
    }
}
