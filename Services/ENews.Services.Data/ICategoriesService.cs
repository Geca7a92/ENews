namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ENews.Data.Models;
    using ENews.Web.ViewModels.Administration.Categories;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAllCategories<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetAllDeletedCategories<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetAllCategoriesWithAnySubCategories<T>();

        Task<int> UpdateCategory(Category category);

        Task UndeleteById(int id);

        Task DeleteById(int id);

        Task HardDeleteById(int id);

        Task CreateCategoryAsync(CategoryCreateInputModel inputModel);

        T GetCategoryByName<T>(string name);

        Task<bool> CategoryExistsById(int id);

        Task<bool> CategoryExistsByName(string name);

        Task<Category> GetCategoryModelById(int id);

        Task<T> GetCategoryById<T>(int id);

        int GetCountOfActiveCategories();

        int GetCountOfDeletedCategories();
    }
}
