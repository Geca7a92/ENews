using ENews.Web.ViewModels.Administration.SubCategories;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface ISubCategoriesService
    {
        IEnumerable<T> GetAllWithDeletedSubCategories<T>();

        Task CreateSubCategoryAsync(SubCategoryCreateInputModel inputModel);

    }
}
