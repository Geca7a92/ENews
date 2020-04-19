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

        Task HardDeleteById(int id);

        Task<bool> SubCategoryExistsByName(string name);

        Task<bool> SubCategoryExistsById(int id);

        Task<T> GetSubCategoryById<T>(int id);

        T GetSubCategoryByName<T>(string name);

        IEnumerable<T> GetSubCategoriesOfCategoryId<T>(int id);
    }
}
