namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Articles;
    using ENews.Web.ViewModels.Categories;

    public class ArticleService : IArticleService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;

        public ArticleService(IDeletableEntityRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
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

        public IEnumerable<T> GetArticlesByCategoryName<T>(string name)
        {
            IQueryable<Article> query
                = this.articleRepository.All().Where(a => a.Category.Title == name).OrderByDescending(a => a.CreatedOn);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetArticlesBySubCategoryName<T>(string name)
        {
            IQueryable<Article> query
                = this.articleRepository.All().Where(a => a.SubCategory.Title == name).OrderByDescending(a => a.CreatedOn);

            return query.To<T>().ToList();
        }

        public T PreviewArticleById<T>(int id)
        {
            var model = this.articleRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefault();
            return model;
        }
    }
}
