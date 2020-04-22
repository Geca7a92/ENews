namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Data.Models.Enums;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.MembersArea.Articles;

    public class ArticlesService : IArticlesService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly IGalleriesService galleryService;
        private readonly ICloudinaryService cloudinaryService;

        public ArticlesService(
            IDeletableEntityRepository<Article> articleRepository,
            IDeletableEntityRepository<Image> imageRepository,
            IGalleriesService galleryService,
            ICloudinaryService cloudinaryService)
        {
            this.articleRepository = articleRepository;
            this.imageRepository = imageRepository;
            this.galleryService = galleryService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<int> CreateAsync(ArticleCreateInputModel model, string userId)
        {
            var mainImageUrl = await this.cloudinaryService.UploadPictureAsync(model.MainImage);
            var mainImage = new Image()
            {
                ImageUrl = mainImageUrl,
                Description = model.MainImage.FileName,
            };

            await this.imageRepository.AddAsync(mainImage);
            await this.imageRepository.SaveChangesAsync();

            var article = new Article
            {
                AuthorId = userId,
                Title = model.Title,
                Content = model.Content,
                CategoryId = model.CategoryId,
                SubCategoryId = model.SubCategoryId,
                PictureId = mainImage.Id,
                Region = model.Region,
            };

            if (model.GalleryContent != null)
            {
                int galleryId = await this.galleryService.CreateAsync(model.GalleryContent, mainImage);
                article.GalleryId = galleryId;
            }

            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveChangesAsync();

            return article.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Article> query
                = this.articleRepository.All().OrderBy(x => x.Title);
            if (count != null)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByAtuthorId<T>(string id)
        {
            IQueryable<Article> query
                = this.articleRepository.AllWithDeleted().Where(a => a.AuthorId == id).OrderByDescending(a => a.CreatedOn);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetLatesByCreatedOn<T>(int? count = null)
        {
            IQueryable<Article> query
                = this.articleRepository.All().OrderByDescending(x => x.CreatedOn).Where(a => !a.Category.IsDeleted || !a.SubCategory.IsDeleted);
            if (count != null)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetArticleById<T>(int id)
        {
            var article = this.articleRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefault();
            return article;
        }

        public IEnumerable<T> GetAllByCategoryId<T>(int id, int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.All()
                .Where(a => a.CategoryId == id)
                .OrderByDescending(a => a.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();
        }

        public IEnumerable<T> GetAllBySubCategoryId<T>(int id, int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.All()
                .Where(a => a.SubCategoryId == id)
                .OrderByDescending(a => a.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();
        }

        public int GetCountByCategoryId(int id)
        {
            return this.articleRepository.All().Where(a => a.CategoryId == id).Count();
        }

        public int GetCountBySubCategoryId(int id)
        {
            return this.articleRepository.All().Where(a => a.SubCategoryId == id).Count();
        }

        public int GetCountByRegion(Region region)
        {
            return this.articleRepository.All().Where(a => a.Region == region).Count();
        }

        public int GetCountOfLocalArticles()
        {
            return this.articleRepository.All().Where(a => a.Region.HasValue).Count();
        }

        public IEnumerable<T> GetLatestByRegion<T>(Region region, int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.All().Where(a => a.Region == region).OrderByDescending(a => a.CreatedOn).Skip(skip);
            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();

        }

        public IEnumerable<T> GetLatesLocalArticles<T>(int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.All().Where(a => a.Region.HasValue).OrderByDescending(a => a.CreatedOn).Skip(skip);
            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();
        }
    }
}
