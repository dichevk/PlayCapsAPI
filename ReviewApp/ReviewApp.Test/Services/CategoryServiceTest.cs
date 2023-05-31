using PlayCapsViewer.Services;


namespace ReviewApp.Test.Services
{
    public class CategoryServiceTest
    {
        private CategoryService _categoryService;
        public async void Init()
        {
            var dbContext = new DbContextTestSetup();
            var dbSetup = await dbContext.GetDatabaseContext();
            _categoryService = new CategoryService(dbSetup);
        }
        [Fact]
        public async void CategoryService_GetAllCategories_ReturnsListOfCategories()
        {
            //Arrange
            Init();
            //Act 
            var result = await _categoryService.GetCategories();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Category>>();
        }
        [Fact]
        public async void CategoryService_GetCategoryById_WithCorrectId_ReturnsCategory()
        {
            //Arrange
            Init();
            //Act 
            var result = await _categoryService.GetCategory(1);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Category>();
        }
        [Fact]
        public async void CategoryService_GetCategoryById_WithIncorrectId_ReturnsNull()
        {
            //Arrange
            Init();
            //Act 
            var result = await _categoryService.GetCategory(-100000);
            //Assert
            result.Should().BeNull();
        }
        [Fact]
        public async void CategoryService_DeleteCategory_WithCorrectId_ReturnsTrue()
        {
            //Arrange
            Init();
            //Act 
            var result = await _categoryService.DeleteCategory(2);
            //Assert
            result.Should().BeTrue();
        }
        [Fact]
        public async void CategoryService_DeleteCategory_WithIncorrectId_ReturnsFalse()
        {
            //Arrange
            Init();
            //Act 
            var result = await _categoryService.DeleteCategory(-100000);
            //Assert
            result.Should().BeFalse();
        }

    }
}
