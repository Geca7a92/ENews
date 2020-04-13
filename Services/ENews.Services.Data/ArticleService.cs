namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.MembersArea.Article;

    public class ArticleService : IArticleService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly IGalleryService galleryService;
        private readonly ICloudinaryService cloudinaryService;

        public ArticleService(
            IDeletableEntityRepository<Article> articleRepository,
            IDeletableEntityRepository<Image> imageRepository,
            IGalleryService galleryService,
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

        public IEnumerable<T> GetLatesByCreatedOn<T>(int? count = null)
        {
            IQueryable<Article> query
                = this.articleRepository.All().OrderByDescending(x => x.CreatedOn);
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
    }
}
