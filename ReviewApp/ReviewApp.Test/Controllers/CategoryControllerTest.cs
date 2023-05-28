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
        public async Task CategoryController_GetCategories_ReturnsAllCategories()
        {
            var categories = A.Fake<ICollection<CategoryDTO>>();
            var categoriesList = A.Fake<List<CategoryDTO>>();
            // Arrange
            var categoryDTOs = categories.Select(c => new CategoryDTO { Id = c.Id, Name = c.Name });
            A.CallTo(() => _mapper.Map<List<CategoryDTO>>(categories)).Returns(categoriesList);

            // Act
            var result = _categoryController.GetCategories();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(categoryDTOs);
        }
        [Fact]
        public async Task CategoryController_GetCategoriesByPlayCap_ReturnsCategoriesForPlayCap()
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
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CategoryController_GetCategoryById_WithValidId_ReturnsCategory()
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
        [Fact]
        public async Task CategoryController_GetCategoryById_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Category 1" };
            A.CallTo(() => _categoryService.GetCategory(categoryId)).Returns(category);

            // Act
            var result = await _categoryController.GetCategoryById(categoryId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CategoryController_GetCategoryById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var categoryId = 1;
            A.CallTo(() => _categoryService.GetCategory(categoryId)).Returns(Task.FromResult<Category>(null));

            // Act
            var result = await _categoryController.GetCategoryById(categoryId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("Category was not found");
        }



        [Fact]
        public async Task CategoryController_GetCategoriesByPlayCap_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var playCapId = 1;
            var categories = new List<Category>(); // Empty categories list
            A.CallTo(() => _categoryService.GetCategoriesByPlayCapId(playCapId)).Returns(categories);

            // Act
            var result = await _categoryController.GetCategoriesByPlayCap(playCapId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("Id of the playcap was not found in correlation with the Category");
        }





        [Fact]
        public async Task CategoryController_DeleteCategory_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            var categoryId = 1;
            A.CallTo(() => _categoryService.DeleteCategory(categoryId)).Returns(true);

            // Act
            var result = await _categoryController.DeleteCategory(categoryId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task CategoryController_DeleteCategory_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var categoryId = 1;
            A.CallTo(() => _categoryService.DeleteCategory(categoryId)).Returns(false);

            // Act
            var result = await _categoryController.DeleteCategory(categoryId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("Category was not found");
        }

        [Fact]
        public async Task CategoryController_UpdateCategory_ValidData_ReturnsOkObjectResult()
        {
            // Arrange
            var categoryDTO = new CategoryDTO { Id = 1, Name = "Updated Category" };
            var category = new Category { Id = categoryDTO.Id, Name = categoryDTO.Name };
            A.CallTo(() => _mapper.Map<Category>(categoryDTO)).Returns(category);
            A.CallTo(() => _categoryService.UpdateCategory(category)).Returns(category);

            // Act
            var result = await _categoryController.UpdateCategory(categoryDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task CategoryController_CreateCategory_ValidData_ReturnsOkObjectResult()
        {
            // Arrange
            var categoryDTO = new CategoryDTO { Id = 1, Name = "Category 1" };
            var category = new Category { Id = categoryDTO.Id, Name = categoryDTO.Name };
            A.CallTo(() => _mapper.Map<Category>(categoryDTO)).Returns(category);
            A.CallTo(() => _categoryService.CreateCategory(category)).Returns(category);

            // Act
            var result = await _categoryController.CreateCategory(categoryDTO);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
