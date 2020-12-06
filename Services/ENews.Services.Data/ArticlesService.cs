﻿namespace ENews.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Data.Models.Enums;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.MembersArea.Articles;
    using Microsoft.EntityFrameworkCore;

    public class ArticlesService : IArticlesService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IImagesService imagesService;
        private readonly IGalleriesService galleryService;

        public ArticlesService(
            IDeletableEntityRepository<Article> articleRepository,
            IImagesService imagesService,
            IGalleriesService galleryService)
        {
            this.articleRepository = articleRepository;
            this.imagesService = imagesService;
            this.galleryService = galleryService;
        }

        public async Task<int> CreateAsync(ArticleCreateInputModel model, string userId)
        {
            var mainImgId = await this.imagesService.CreateAsync(model.MainImage);
            var article = new Article
            {
                AuthorId = userId,
                Title = model.Title,
                Content = model.Content,
                CategoryId = model.CategoryId,
                SubCategoryId = model.SubCategoryId,
                PictureId = mainImgId.Id,
                Region = model.Region,
                VideoUrl = model.VideoUrl,
            };

            if (model.GalleryContent != null)
            {
                int galleryId = await this.galleryService.CreateAsync(model.GalleryContent, mainImgId);
                article.GalleryId = galleryId;
            }

            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveChangesAsync();

            return article.Id;
        }

        public IEnumerable<T> GetAllByAtuthorId<T>(string id, int? take = null, int skip = 0)
        {
            IQueryable<Article> query
                = this.articleRepository.All().Where(a => a.AuthorId == id).OrderByDescending(a => a.CreatedOn).Skip(skip);
            if (take != null)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByAtuthorIdDeleted<T>(string id, int? take = null, int skip = 0)
        {
            IQueryable<Article> query
                = this.articleRepository.AllWithDeleted().Where(a => a.AuthorId == id && a.IsDeleted).OrderByDescending(a => a.CreatedOn).Skip(skip);

            if (take != null)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetLatesByCreatedOn<T>(int? take = null, int skip = 0)
        {
            IQueryable<Article> query
                = this.articleRepository.All().OrderByDescending(x => x.CreatedOn).Skip(skip);
            if (take != null)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task<Article> GetbyIdHard(int id)
        {
            var article = await this.articleRepository.GetByIdWithDeletedAsync(id);
            return article;
        }

        public async Task<T> GetArticleById<T>(int id)
        {
            var article = await this.articleRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return article;
        }

        public async Task<T> GetArticleByIdWithDeleted<T>(int id)
        {
            var article = await this.articleRepository.AllWithDeleted().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return article;
        }

        public IEnumerable<T> GetAllByCategoryId<T>(int id, int? take = null, int skip = 0, int? acrticleId = null)
        {
            var articles = this.articleRepository.All()
                .Where(a => a.CategoryId == id && a.Id != acrticleId)
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

        public int GetCountByAuthorId(string id)
        {
            return this.articleRepository.All().Where(a => a.AuthorId == id).Count();
        }

        public int GetCountByAuthorIdDeleted(string id)
        {
            return this.articleRepository.AllWithDeleted().Where(a => a.AuthorId == id && a.IsDeleted).Count();
        }

        public int GetCount()
        {
            return this.articleRepository.All().Count();
        }

        public int GetSumAllSeens()
        {
            return this.articleRepository.All().Sum(a => a.SeenCount);
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

        public IEnumerable<T> GetLatesMostViewed<T>(int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.All().OrderByDescending(a => a.SeenCount).Where(a => a.CreatedOn.AddDays(7) > DateTime.UtcNow).Skip(skip);
            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();
        }

        public IEnumerable<T> GetLatesMostCommented<T>(int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.All().OrderByDescending(a => a.Comments.Count()).Where(a => a.CreatedOn.AddDays(7) > DateTime.UtcNow && a.Comments.Count() > 0).Skip(skip);
            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();
        }

        public bool ArticleExist(int id)
        {
            return this.articleRepository.All().Any(a => a.Id == id);
        }

        public DateTime LastesArticleCreationDate()
        {
            return this.articleRepository.All().OrderByDescending(a => a.CreatedOn).First().CreatedOn;
        }

        public async Task DeleteById(int id)
        {
            var article = await this.articleRepository.GetByIdWithDeletedAsync(id);
            this.articleRepository.Delete(article);
            await this.articleRepository.SaveChangesAsync();
        }

        public async Task UndeleteById(int id)
        {
            var article = await this.articleRepository.GetByIdWithDeletedAsync(id);
            this.articleRepository.Undelete(article);
            await this.articleRepository.SaveChangesAsync();
        }

        public async Task HardDeleteById(int id)
        {
            var article = await this.articleRepository.GetByIdWithDeletedAsync(id);
            this.articleRepository.HardDelete(article);
            await this.articleRepository.SaveChangesAsync();
        }

        public async Task<int> AddToSeenCount(int id)
        {
            var article = await this.articleRepository.GetByIdWithDeletedAsync(id);
            article.SeenCount++;
            await this.articleRepository.SaveChangesAsync();
            return article.SeenCount;
        }

        public IEnumerable<T> GetLatesInternationalArticles<T>(int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.All().Where(a => !a.Region.HasValue).OrderByDescending(a => a.CreatedOn).Skip(skip);
            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();
        }

        public IEnumerable<T> GetLatesWithVideos<T>(int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.All().Where(a => !string.IsNullOrEmpty(a.VideoUrl)).OrderByDescending(a => a.CreatedOn).Skip(skip);
            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();
        }

        public T GetLastByCreatedOn<T>()
        {
            var article = this.articleRepository.All().OrderByDescending(a => a.CreatedOn).To<T>().FirstOrDefault();
            return article;
        }

        public IEnumerable<T> GetAllActive<T>(int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.All().OrderByDescending(a => a.CreatedOn).Skip(skip);

            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();
        }

        public IEnumerable<T> GetAllDeleted<T>(int? take = null, int skip = 0)
        {
            var articles = this.articleRepository.AllWithDeleted()
                .Where(a => a.IsDeleted)
                .OrderByDescending(a => a.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                articles = articles.Take(take.Value);
            }

            return articles.To<T>().ToList();
        }
    }
}
