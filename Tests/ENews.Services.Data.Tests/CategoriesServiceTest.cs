namespace ENews.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;
    using ENews.Data.Models;
    using ENews.Data.Repositories;
    using ENews.Services.Data.Tests.Models;
    using ENews.Services.Data.Tests.Repositories;
    using ENews.Services.Data.Tests.Seed;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Administration.Categories;
    using Xunit;

    public class CategoriesServiceTest
    {
        public CategoriesServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(CategoryDumyModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetAllCategoriesSingleResultTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllCategories<CategoryDumyModel>();

            Assert.Single(result);
        }

        [Fact]
        public void GetAllCategoriesTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllCategories<CategoryDumyModel>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllDeletedCategoriesTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllDeletedCategories<CategoryDumyModel>();

            Assert.Single(result);
        }

        [Fact]
        public void GetAllCategoriesWithAnySubCategoriesTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllCategoriesWithAnySubCategories<CategoryDumyModel>();

            Assert.Single(result);
        }

        [Fact]
        public void UpdateCategoryTest()
        {
            string newName = "New name";
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var service = this.GetArticlesService(repository);

            category.Title = newName;

            var result = service.UpdateCategory(category);

            Assert.Equal(newName, category.Title);
        }

        [Fact]
        public void UndeleteByIdTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var service = this.GetArticlesService(repository);
            repository.Delete(category);

            var result = service.UndeleteById(category.Id);

            Assert.False(category.IsDeleted);
        }

        [Fact]
        public void DeleteByIdTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var service = this.GetArticlesService(repository);
            var result = service.DeleteById(category.Id);

            Assert.True(category.IsDeleted);
        }

        [Fact]
        public void HardDeleteByIdTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var service = this.GetArticlesService(repository);

            service.HardDeleteById(category.Id).GetAwaiter();

            Assert.Equal(0, repository.AllWithDeleted().Count());
        }

        [Fact]
        public void CreateCategoryAsyncTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var service = this.GetArticlesService(repository);
            service.CreateCategoryAsync(new CategoryCreateInputModel { Title = "New category" }).GetAwaiter();

            Assert.Equal(2, repository.All().Count());
        }

        [Fact]
        public void GetCategoryByNameOneResultTest()
        {
            string categoryName = "One";

            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetCategoryByName<CategoryDumyModel>(categoryName).GetAwaiter().GetResult();

            Assert.Equal(category.Title, result.Title);
        }

        [Fact]
        public void CategoryExistsByIdTrueTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.CategoryExistsById(category.Id).GetAwaiter().GetResult();

            Assert.True(result);
        }

        [Fact]
        public void CategoryExistsByIdFalseTest()
        {

            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.CategoryExistsById(55).GetAwaiter().GetResult();

            Assert.False(result);
        }

        [Fact]
        public void CategoryExistsByNameFalseTest()
        {
            var name = "someName";
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.CategoryExistsByName(name).GetAwaiter().GetResult();

            Assert.False(result);
        }

        [Fact]
        public void CategoryExistsByNameTrueTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.CategoryExistsByName(category.Title).GetAwaiter().GetResult();

            Assert.True(result);
        }

        [Fact]
        public void GetCategoryModelByIdTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetCategoryModelById(1).GetAwaiter().GetResult();

            Assert.Equal(category, result);
        }

        [Fact]
        public void GetCategoryModelByFakeIdTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetCategoryModelById(55).GetAwaiter().GetResult();

            Assert.Null(result);
        }

        [Fact]
        public void GetCategoryByIdFakeTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetCategoryById<CategoryDumyModel>(int.MaxValue).GetAwaiter().GetResult();

            Assert.Null(result);
        }

        [Fact]
        public void GetCategoryByIdTest()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetCategoryById<CategoryDumyModel>(1).GetAwaiter().GetResult();

            Assert.Equal(category.Id, result.Id);
        }

        [Fact]
        public void GetCountOfActiveCategories()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountOfActiveCategories();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetCountOfDeletedCategories()
        {
            var repository = CategoryRepository.Create();
            var category = this.CreateArticle(repository, SeedCategory.Create());
            var secondCategory = this.CreateArticle(repository, SeedCategory.CreateSecond());
            var thirdCategory = this.CreateArticle(repository, SeedCategory.CreateThird());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountOfDeletedCategories();

            Assert.Equal(1, result);
        }

        private CategoriesService GetArticlesService(EfDeletableEntityRepository<Category> repository)
        {
            var service = new CategoriesService(repository);
            return service;
        }

        private Category CreateArticle(EfDeletableEntityRepository<Category> repository, Category category)
        {
            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            return category;
        }
    }
}
