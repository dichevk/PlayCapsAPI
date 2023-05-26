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
        [Fact]
        public async Task GetCategoriesByPlayCap_ReturnsCategoriesForPlayCap()
        {
            // Arrange
            int playCapId = 1;
            var categories = A.Fake<ICollection<CategoryDTO>>();
            var categoriesList = A.Fake<List<CategoryDTO>>();
            var categories2 = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };
            var categoryDTOs = categories.Select(c => new CategoryDTO { Id = c.Id, Name = c.Name });
            A.CallTo(() => _mapper.Map<List<CategoryDTO>>(categories)).Returns(categoriesList);

            // Act
            var result = await _categoryController.GetCategoriesByPlayCap(playCapId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(categoryDTOs);
        }

        [Fact]
        public async Task GetCategoryById_WithValidId_ReturnsCategory()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Category 1" };
            var categoryDTO = new CategoryDTO { Id = categoryId, Name = "Category 1" };
            A.CallTo(() => _categoryService.GetCategory(categoryId)).Returns(category);
            A.CallTo(() => _mapper.Map<CategoryDTO>(category)).Returns(categoryDTO);

            // Act
            var result = await _categoryController.GetCategoryById(categoryId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(categoryDTO);
        }
    }
}
