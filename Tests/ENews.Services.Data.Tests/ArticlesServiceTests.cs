namespace ENews.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;

    using ENews.Data.Models;
    using ENews.Data.Models.Enums;
    using ENews.Data.Repositories;
    using ENews.Services.Data.Tests.Models;
    using ENews.Services.Data.Tests.Repositories;
    using ENews.Services.Data.Tests.Seed;
    using ENews.Services.Mapping;
    using Moq;
    using Xunit;

    public class ArticlesServiceTests
    {
        public ArticlesServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetArticleById()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);
            var result = service.GetArticleById<ArticleDumyModel>(article.Id).GetAwaiter().GetResult();

            Assert.Equal(article.Title, result.Title);
        }

        [Fact]
        public void GetCount()
        {
            var repository = ArticleRepository.Create();
            this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);
            var result = service.GetCount();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetSumAllSeensTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetSumAllSeens();

            Assert.Equal(3, result);
        }

        [Fact]
        public void DeleteByIdTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);

            service.DeleteById(article.Id).GetAwaiter();
            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public void AddToSeenCountTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);
            var seenCount = article.SeenCount;

            service.AddToSeenCount(article.Id).GetAwaiter().GetResult();

            Assert.Equal(seenCount + 1, article.SeenCount);
        }

        //bool ArticleExist(int id);

        [Fact]
        public void ArticleExistTrueTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);
            var result = service.ArticleExist(article.Id);

            Assert.True(result);
        }

        [Fact]
        public void ArticleExistFalseTest()
        {
            var repository = ArticleRepository.Create();
            var service = this.GetArticlesService(repository);
            var result = service.ArticleExist(1);

            Assert.False(result);
        }

        [Fact]
        public void GetCountByCategoryIdTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountByCategoryId(1);

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountBySubCategoryIdOneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountBySubCategoryId(1);

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountBySubCategoryIdNoMatchTest()
        {
            var repository = ArticleRepository.Create();
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountBySubCategoryId(1);

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountOfLocalArticlesNoResultTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountOfLocalArticles();

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountOfLocalArticlesOneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var articleWithRegion = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountOfLocalArticles();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountByAuthorRealIdTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountByAuthorId("ads");

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountByAuthorIdFakeIdTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountByAuthorId("FAKE");

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountByAuthorIdDeletedRealIdTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);

            var result = service.GetCountByAuthorIdDeleted("ads");

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountByAuthorIdDeletedFakeIdTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountByAuthorIdDeleted("FAKE");

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountByRegionOneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountByRegion(Region.Burgas);

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountByRegionNoMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetCountByRegion(Region.Sofia);

            Assert.Equal(0, result);
        }

        [Fact]
        public void LastesArticleCreationDateTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.LastesArticleCreationDate();

            Assert.Equal(secondArticle.CreatedOn, result);
        }

        [Fact]
        public void GetLatestByRegionOneMatchTest()
        {
            var region = Region.Burgas;
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatestByRegion<ArticleDumyModel>(region);

            Assert.Single(result);
        }

        [Fact]
        public void GetLatestByRegionNoMatcheshTest()
        {
            var region = Region.Sofia_District;
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatestByRegion<ArticleDumyModel>(region);

            Assert.Empty(result);
        }

        [Fact]
        public void GetAllByCategoryIdOneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllByCategoryId<ArticleDumyModel>(1);

            Assert.Single(result);
        }

        [Fact]
        public void GetAllByCategoryIdNoMatchesTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllByCategoryId<ArticleDumyModel>(5);

            Assert.Empty(result);
        }

        [Fact]
        public void GetAllBySubCategoryIdNoMatchesTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllBySubCategoryId<ArticleDumyModel>(5);

            Assert.Empty(result);
        }

        [Fact]
        public void GetAllBySubCategoryIdOneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllBySubCategoryId<ArticleDumyModel>(1);

            Assert.Single(result);
        }

        [Fact]
        public void GetAllByAtuthorIdOneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllByAtuthorId<ArticleDumyModel>(article.AuthorId);

            Assert.Single(result);
        }

        [Fact]
        public void GetAllByAtuthorIdNoMatchesTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllByAtuthorId<ArticleDumyModel>("FAKENAME");

            Assert.Empty(result);
        }

        [Fact]
        public void GetLatesByCreatedTwoMatchesOnTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatesByCreatedOn<ArticleDumyModel>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetLatesWithVideosOneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatesWithVideos<ArticleDumyModel>();

            Assert.Single(result);
        }

        [Fact]
        public void GetLatesWithVideosNoMatchesTest()
        {
            var repository = ArticleRepository.Create();
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatesWithVideos<ArticleDumyModel>();

            Assert.Empty(result);
        }

        [Fact]
        public void UndeleteByIdTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());

            repository.Delete(article);
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            Assert.Equal(1, repository.All().Count());

            var service = this.GetArticlesService(repository);

            service.UndeleteById(article.Id).GetAwaiter();

            Assert.Equal(2, repository.All().Count());
        }

        [Fact]
        public void GetAllByAtuthorIdDeletedOneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());

            repository.Delete(article);
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = this.GetArticlesService(repository);
            var result = service.GetAllByAtuthorIdDeleted<ArticleDumyModel>(article.AuthorId);

            Assert.Single(result);
        }

        [Fact]
        public void GetAllByAtuthorIdDeletedNoMatchesTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetAllByAtuthorIdDeleted<ArticleDumyModel>(article.AuthorId);

            Assert.Empty(result);
        }

        [Fact]
        public void GetLatesMostViewedTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatesMostViewed<ArticleDumyModel>();

            Assert.Equal(secondArticle.Id, result.First().Id);
        }

        [Fact]
        public void GetLatesInternationalArticlesTwoMatchesTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var thirdArticle = this.CreateArticle(repository, SeedArticle.CreateThirdArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatesInternationalArticles<ArticleDumyModel>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetLatesInternationalArticlesoneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var thirdArticle = this.CreateArticle(repository, SeedArticle.CreateThirdArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatesInternationalArticles<ArticleDumyModel>(1);

            Assert.Single(result);
        }

        [Fact]
        public void GetLatesLocalArticlesOneMatchTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var thirdArticle = this.CreateArticle(repository, SeedArticle.CreateThirdArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatesLocalArticles<ArticleDumyModel>();

            Assert.Single(result);
        }

        [Fact]
        public void GetLatesMostCommentedTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var thirdArticle = this.CreateArticle(repository, SeedArticle.CreateThirdArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLatesMostCommented<ArticleDumyModel>();

            Assert.Empty(result);
        }

        [Fact]
        public void GetLastByCreatedOnTest()
        {
            var repository = ArticleRepository.Create();
            var article = this.CreateArticle(repository, SeedArticle.Create());
            var secondArticle = this.CreateArticle(repository, SeedArticle.CreateSecondArticle());
            var thirdArticle = this.CreateArticle(repository, SeedArticle.CreateThirdArticle());
            var service = this.GetArticlesService(repository);
            var result = service.GetLastByCreatedOn<ArticleDumyModel>();

            Assert.Equal(thirdArticle.Id, result.Id);
        }

        private ArticlesService GetArticlesService(EfDeletableEntityRepository<Article> repository)
        {
            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();
            var usersMoq = new Mock<IUsersService>();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object, usersMoq.Object);

            return service;
        }

        private Article CreateArticle(EfDeletableEntityRepository<Article> repository, Article article)
        {
            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            return article;
        }

        //Task<int> CreateAsync(ArticleCreateInputModel model, string userId);
    }
}
