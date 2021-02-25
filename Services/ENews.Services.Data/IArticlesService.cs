namespace ENews.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ENews.Data.Models;
    using ENews.Data.Models.Enums;
    using ENews.Web.ViewModels.MembersArea.Articles;

    public interface IArticlesService
    {
        IEnumerable<T> GetAllActive<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetAllDeleted<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetAllByCategoryId<T>(int id, int? take = null, int skip = 0, int? acrticleId = null);

        IEnumerable<T> GetAllBySubCategoryId<T>(int id, int? take = null, int skip = 0);

        IEnumerable<T> GetAllByAtuthorId<T>(string id, int? take = null, int skip = 0);

        IEnumerable<T> GetAllByAtuthorIdDeleted<T>(string id, int? take = null, int skip = 0);

        //Org
        IEnumerable<T> GetLatesByCreatedOn<T>(int? take = null, int skip = 0);

        //Test
        Task<IEnumerable<T>> GetLatesByCreatedOnAsync<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatesWithVideos<T>(int? take = null, int skip = 0);

        //Test
        Task<IEnumerable<T>> GetLatesWithVideosAsync<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatesMostViewed<T>(int? take = null, int skip = 0);

        //Test
        Task<IEnumerable<T>> GetLatesMostViewedAsync<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatestByRegion<T>(Region region, int? take = null, int skip = 0);

        IEnumerable<T> GetLatesInternationalArticles<T>(int? take = null, int skip = 0);

        //Test
        Task<IEnumerable<T>> GetLatesInternationalArticlesAsync<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatesLocalArticles<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatesMostCommented<T>(int? take = null, int skip = 0);

        //Eddited
        Task<T> GetLastByCreatedOn<T>();

        bool ArticleExist(int id);

        Task<int> AddToSeenCount(int id);

        Task UndeleteById(int id);

        Task DeleteById(int id);

        Task HardDeleteById(int id);

        int GetCountOfLocalArticles();

        int GetCount();

        int GetCountByCategoryId(int id);

        int GetCountBySubCategoryId(int id);

        int GetCountByAuthorId(string id);

        int GetCountByAuthorIdDeleted(string id);

        int GetCountByRegion(Region region);

        int GetSumAllSeens();

        Task<bool> CheckArticleOwnership(string userId, int articleId);

        Task<Article> GetbyIdHard(int id);

        Task<T> GetArticleById<T>(int id);

        Task<T> GetArticleByIdWithDeleted<T>(int id);

        Task<int> CreateAsync(ArticleCreateInputModel model, string userId);

        Task<int> Update(ArticleCreateInputModel model, int articleId);

        DateTime LastesArticleCreationDate();
    }
}
