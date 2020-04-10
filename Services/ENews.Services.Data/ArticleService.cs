namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Articles;
    using ENews.Web.ViewModels.Categories;

    public class ArticleService : IArticleService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ArticleService(
            IDeletableEntityRepository<Article> articleRepository,
            IDeletableEntityRepository<Image> imageRepository,
            ICloudinaryService cloudinaryService)
        {
            this.articleRepository = articleRepository;
            this.imageRepository = imageRepository;
            this.cloudinaryService = cloudinaryService;
        }

        //To DO fix
        public async Task<int> CreateAsync(ArticleCreateInputModel model, string userId)
        {
            var imageUrl = this.cloudinaryService.UploadPictureAsync(model.MainImage);
            var mainImage = new Image()
            {
                ImageUrl = imageUrl.Result,
                Description = imageUrl.Result,
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

        public T PreviewArticleById<T>(int id)
        {
            var model = this.articleRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefault();
            return model;
        }
    }
}
