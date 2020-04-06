using ENews.Web.ViewModels.Articles;
using ENews.Web.ViewModels.Categories;
using System.Collections.Generic;
using System.Text;

namespace ENews.Services.Data
{
    public interface IArticleService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetLatesByCreatedOn<T>(int? count = null);

        IEnumerable<T> GetArticlesBySubCategoryName<T>(string name);

        IEnumerable<T> GetArticlesByCategoryName<T>(string name);

        T PreviewArticleById<T>(int id);
    }
}
