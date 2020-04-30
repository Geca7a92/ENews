using ENews.Data.Models;
using ENews.Data.Models.Enums;
using ENews.Services.Data.Tests.Repositories;
using ENews.Services.Data.Tests.Seed;
using ENews.Services.Mapping;
using ENews.Web.ViewModels;
using ENews.Web.ViewModels.Comments;
using ENews.Web.ViewModels.MembersArea.Articles;
using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace ENews.Services.Data.Tests
{
    public class ArticlesServiceTests
    {
        [Fact]
        public void GetArticleById()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetArticleById<ArticleDumyModel>(article.Id).GetAwaiter().GetResult();

            Assert.Equal(article.Title, result.Title);
        }

        [Fact]
        public void GetCount()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetCount();
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetSumAllSeensTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetSumAllSeens();

            Assert.Equal(3, result);
        }

        [Fact]
        public void DeleteByIdTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            service.DeleteById(article.Id).GetAwaiter();
            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public void AddToSeenCountTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var seenCount = article.SeenCount;

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            service.AddToSeenCount(article.Id);

            Assert.Equal(seenCount + 1, article.SeenCount);
        }
        //bool ArticleExist(int id);

        [Fact]
        public void ArticleExistTrueTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();


            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.ArticleExist(article.Id);

            Assert.True(result);
        }

        [Fact]
        public void ArticleExistFalseTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.ArticleExist(article.Id);

            Assert.False(result);
        }

        [Fact]
        public void GetCountByCategoryIdTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();


            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountByCategoryId(1);

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountBySubCategoryIdOneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountBySubCategoryId(1);

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountBySubCategoryIdNoMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountBySubCategoryId(1);

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountOfLocalArticlesNoResultTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountOfLocalArticles();

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountOfLocalArticlesOneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var articleWithRegion = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(articleWithRegion).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountOfLocalArticles();

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountByAuthorRealIdTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountByAuthorId("ads");

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountByAuthorIdFakeIdTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountByAuthorId("FAKE");

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountByAuthorIdDeletedRealIdTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountByAuthorIdDeleted("ads");

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountByAuthorIdDeletedFakeIdTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountByAuthorIdDeleted("FAKE");

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetCountByRegionOneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountByRegion(Region.Burgas);

            Assert.Equal(1, result);
        }

        [Fact]
        public void GetCountByRegionNoMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.GetCountByRegion(Region.Sofia);

            Assert.Equal(0, result);
        }

        [Fact]
        public void LastesArticleCreationDateTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);
            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);

            var result = service.LastesArticleCreationDate();

            Assert.Equal(secondArticle.CreatedOn, result);
        }

        [Fact]
        public void GetLatestByRegionOneMatchTest()
        {
            var region = Region.Burgas;

            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatestByRegion<ArticleDumyModel>(region);

            Assert.Single(result);
        }

        [Fact]
        public void GetLatestByRegionNoMatcheshTest()
        {
            var region = Region.Sofia_District;

            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatestByRegion<ArticleDumyModel>(region);

            Assert.Empty(result);
        }

        [Fact]
        public void GetAllByCategoryIdOneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetAllByCategoryId<ArticleDumyModel>(1);

            Assert.Single(result);
        }

        [Fact]
        public void GetAllByCategoryIdNoMatchesTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetAllByCategoryId<ArticleDumyModel>(5);

            Assert.Empty(result);
        }

        [Fact]
        public void GetAllBySubCategoryIdNoMatchesTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetAllBySubCategoryId<ArticleDumyModel>(5);

            Assert.Empty(result);
        }

        [Fact]
        public void GetAllBySubCategoryIdOneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetAllBySubCategoryId<ArticleDumyModel>(1);

            Assert.Single(result);
        }

        [Fact]
        public void GetAllByAtuthorIdOneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetAllByAtuthorId<ArticleDumyModel>(article.AuthorId);

            Assert.Single(result);
        }

        [Fact]
        public void GetAllByAtuthorIdNoMatchesTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetAllByAtuthorId<ArticleDumyModel>("FAKENAME");

            Assert.Empty(result);
        }

        [Fact]
        public void GetLatesByCreatedTwoMatchesOnTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatesByCreatedOn<ArticleDumyModel>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetLatesWithVideosOneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatesWithVideos<ArticleDumyModel>();

            Assert.Single(result);
        }

        [Fact]
        public void GetLatesWithVideosNoMatchesTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatesWithVideos<ArticleDumyModel>();

            Assert.Empty(result);
        }

        [Fact]
        public void UndeleteByIdTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            repository.Delete(article);
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            Assert.Equal(1, repository.All().Count());

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            service.UndeleteById(article.Id).GetAwaiter();

            Assert.Equal(2, repository.All().Count());
        }

        [Fact]
        public void GetAllByAtuthorIdDeletedOneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            repository.Delete(article);
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetAllByAtuthorIdDeleted<ArticleDumyModel>(article.AuthorId);

            Assert.Single(result);
        }

        [Fact]
        public void GetAllByAtuthorIdDeletedNoMatchesTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetAllByAtuthorIdDeleted<ArticleDumyModel>(article.AuthorId);

            Assert.Empty(result);
        }

        [Fact]
        public void GetLatesMostViewedTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatesMostViewed<ArticleDumyModel>();

            Assert.Equal(secondArticle.Id, result.First().Id);
        }

        [Fact]
        public void GetLatesInternationalArticlesTwoMatchesTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();
            var thirdArticle = SeedArticle.CreateThirdArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.AddAsync(thirdArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatesInternationalArticles<ArticleDumyModel>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetLatesInternationalArticlesoneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();
            var thirdArticle = SeedArticle.CreateThirdArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.AddAsync(thirdArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatesInternationalArticles<ArticleDumyModel>(1);

            Assert.Single(result);
        }

        [Fact]
        public void GetLatesLocalArticlesOneMatchTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();
            var thirdArticle = SeedArticle.CreateThirdArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.AddAsync(thirdArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatesLocalArticles<ArticleDumyModel>();

            Assert.Single(result);
        }

        [Fact]
        public void GetLatesMostCommentedTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();
            var thirdArticle = SeedArticle.CreateThirdArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.AddAsync(thirdArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLatesMostCommented<ArticleDumyModel>();

            Assert.Empty(result);
        }

        [Fact]
        public void GetLastByCreatedOnTest()
        {
            var repository = ArticleRepository.Create();

            var imagesMoq = new Mock<IImagesService>();
            var galleriesMoq = new Mock<IGalleriesService>();

            var article = SeedArticle.Create();
            var secondArticle = SeedArticle.CreateSecondArticle();
            var thirdArticle = SeedArticle.CreateThirdArticle();

            repository.AddAsync(article).GetAwaiter().GetResult();
            repository.AddAsync(secondArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            repository.AddAsync(thirdArticle).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var service = new ArticlesService(repository, imagesMoq.Object, galleriesMoq.Object);

            AutoMapperConfig.RegisterMappings(typeof(ArticleDumyModel).GetTypeInfo().Assembly);
            var result = service.GetLastByCreatedOn<ArticleDumyModel>();

            Assert.Equal(thirdArticle.Id, result.Id);
        }

        //Task<int> CreateAsync(ArticleCreateInputModel model, string userId);
    }

    public class ArticleDumyModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryTitle { get; set; }

        public int CategoryId { get; set; }

        public string SubCategoryTitle { get; set; }

        public string PictureImageUrl { get; set; }

        public int SeenCount { get; set; }

        public int CommentsCount { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string AuthorBiography { get; set; }

        public string AuthorUserName { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorProfilePictureImageUrl { get; set; }

        public string AuthorLastName { get; set; }

        public int AuthorArticlesCount { get; set; }

        public DateTime AuthorCreatedOn { get; set; }

        public string AuthorFullName => $"{this.AuthorFirstName} {this.AuthorLastName}";

        public DateTime CreatedOn { get; set; }

        public int? GalleryId { get; set; }

        public CreateCommentInputModel CommentModel { get; set; }
    }
}
