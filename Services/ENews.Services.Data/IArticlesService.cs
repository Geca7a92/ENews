using ENews.Data.Models.Enums;
using ENews.Web.ViewModels.MembersArea.Articles;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface IArticlesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllByCategoryId<T>(int id, int? take = null, int skip = 0);

        IEnumerable<T> GetAllBySubCategoryId<T>(int id, int? take = null, int skip = 0);

        IEnumerable<T> GetAllByAtuthorId<T>(string id);

        IEnumerable<T> GetLatesByCreatedOn<T>(int? count = null);

        IEnumerable<T> GetLatestByRegion<T>(Region region, int? take = null, int skip = 0);

        IEnumerable<T> GetLatesLocalArticles<T>(int? take = null, int skip = 0);

        bool ArticleExist(int id);

        int GetCountOfLocalArticles();

        int GetCountByCategoryId(int id);

        int GetCountBySubCategoryId(int id);

        int GetCountByRegion(Region region);

        T GetArticleById<T>(int id);

        Task<int> CreateAsync(ArticleCreateInputModel model, string userId);
    }
}
