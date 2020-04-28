using ENews.Data.Models.Enums;
using ENews.Web.ViewModels.MembersArea.Articles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface IArticlesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllByCategoryId<T>(int id, int? take = null, int skip = 0, int? acrticleId = null);

        IEnumerable<T> GetAllBySubCategoryId<T>(int id, int? take = null, int skip = 0);

        IEnumerable<T> GetAllByAtuthorId<T>(string id);

        IEnumerable<T> GetLatesByCreatedOn<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatesWithVideos<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatesMostViewed<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatestByRegion<T>(Region region, int? take = null, int skip = 0);

        IEnumerable<T> GetLatesInternationalArticles<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatesLocalArticles<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLatesMostCommented<T>(int? take = null, int skip = 0);

        T GetLastByCreatedOn<T>();

        bool ArticleExist(int id);

        Task<int> AddToSeenCount(int id);

        int GetCountOfLocalArticles();

        int GetCount();

        int GetCountByCategoryId(int id);

        int GetCountBySubCategoryId(int id);

        int GetCountByRegion(Region region);

        int GetSumAllSeens();

        T GetArticleById<T>(int id);

        Task<int> CreateAsync(ArticleCreateInputModel model, string userId);

        DateTime LastesArticleCreationDate();
    }
}
