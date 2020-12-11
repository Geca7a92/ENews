namespace ENews.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;

    using ENews.Services.Data.Tests.Repositories;
    using ENews.Services.Data.Tests.Seed;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Administration.Categories;
    using Xunit;

    public class CategoriesServiceTest
    {
        [Fact]
        public void GetAllCategoriesSingleResultTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.GetAllCategories<IndexCategoryViewModel>();

            Assert.Single(result);
        }

        [Fact]
        public void GetAllCategoriesTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.GetAllCategories<IndexCategoryViewModel>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllDeletedCategoriesTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.GetAllDeletedCategories<IndexCategoryViewModel>();

            Assert.Single(result);
        }

        [Fact]
        public void GetAllCategoriesWithAnySubCategoriesTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.GetAllCategoriesWithAnySubCategories<IndexCategoryViewModel>();

            Assert.Single(result);
        }

        [Fact]
        public void UpdateCategoryTest()
        {
            string newName = "New name";
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            category.Title = newName;
            var service = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.UpdateCategory(category);

            Assert.Equal(newName, category.Title);
        }

        [Fact]
        public void UndeleteByIdTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);
            repository.Delete(category);
            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.UndeleteById(category.Id);

            Assert.False(category.IsDeleted);
        }

        [Fact]
        public void DeleteByIdTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.DeleteById(category.Id);

            Assert.True(category.IsDeleted);
        }

        [Fact]
        public void HardDeleteByIdTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            service.HardDeleteById(category.Id).GetAwaiter();

            Assert.Equal(0, repository.AllWithDeleted().Count());
        }

        [Fact]
        public void CreateCategoryAsyncTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(CategoryCreateInputModel).GetTypeInfo().Assembly);
            service.CreateCategoryAsync(new CategoryCreateInputModel { Title = "New category" }).GetAwaiter();

            Assert.Equal(2, repository.All().Count());
        }

        [Fact]
        public void GetCategoryByNameOneResultTest()
        {
            string categoryName = "One";

            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.GetCategoryByName<IndexCategoryViewModel>(categoryName).GetAwaiter().GetResult();

            Assert.Equal(category.Title, result.Title);
        }

        [Fact]
        public void CategoryExistsByIdTrueTest()
        {

            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            var result = service.CategoryExistsById(category.Id).GetAwaiter().GetResult();

            Assert.True(result);
        }

        [Fact]
        public void CategoryExistsByIdFalseTest()
        {

            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            var result = service.CategoryExistsById(55).GetAwaiter().GetResult();

            Assert.False(result);
        }

        [Fact]
        public void CategoryExistsByNameFalseTest()
        {
            var name = "someName";
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            var result = service.CategoryExistsByName(name).GetAwaiter().GetResult();

            Assert.False(result);
        }

        [Fact]
        public void CategoryExistsByNameTrueTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            var result = service.CategoryExistsByName(category.Title).GetAwaiter().GetResult();

            Assert.True(result);
        }

        [Fact]
        public void GetCategoryModelByIdTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            var result = service.GetCategoryModelById(1).GetAwaiter().GetResult();

            Assert.Equal(category, result);
        }

        [Fact]
        public void GetCategoryModelByFakeIdTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            var result = service.GetCategoryModelById(55).GetAwaiter().GetResult();

            Assert.Null(result);
        }

        [Fact]
        public void GetCategoryByIdFakeTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.GetCategoryById<IndexCategoryViewModel>(int.MaxValue).GetAwaiter().GetResult();

            Assert.Null(result);
        }

        [Fact]
        public void GetCategoryByIdTest()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);

            AutoMapperConfig.RegisterMappings(typeof(IndexCategoryViewModel).GetTypeInfo().Assembly);
            var result = service.GetCategoryById<IndexCategoryViewModel>(1).GetAwaiter().GetResult();

            Assert.Equal(category.Id, result.Id);
        }

        [Fact]
        public void GetCountOfActiveCategories()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);
            var result = service.GetCountOfActiveCategories();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetCountOfDeletedCategories()
        {
            var repository = CategoryRepository.Create();

            var category = SeedCategory.Create();
            var secondCategory = SeedCategory.CreateSecond();
            var thirdCategory = SeedCategory.CreateThird();

            repository.AddAsync(category).GetAwaiter().GetResult();
            repository.AddAsync(secondCategory).GetAwaiter().GetResult();
            repository.AddAsync(thirdCategory).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new CategoriesService(repository);
            var result = service.GetCountOfDeletedCategories();

            Assert.Equal(1, result);
        }

    }
}
