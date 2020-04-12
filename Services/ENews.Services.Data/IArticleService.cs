using ENews.Web.ViewModels.MembersArea.Article;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface IArticleService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetLatesByCreatedOn<T>(int? count = null);

        T GetArticleById<T>(int id);

        Task<int> CreateAsync(ArticleCreateInputModel model, string userId);
    }
}
