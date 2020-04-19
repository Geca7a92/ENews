﻿using ENews.Web.ViewModels.Administration.Categories;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface ICategoriesService
    {
        IEnumerable<T> GetAllCategories<T>();

        IEnumerable<T> GetAllCategoriesWithAnySubCategories<T>();

        IEnumerable<T> GetAllWithDeletedCategories<T>();

        Task UndeleteById(int id);

        Task DeleteById(int id);

        Task HardDeleteById(int id);

        Task CreateCategoryAsync(CategoryCreateInputModel inputModel);

        T GetCategoryByName<T>(string name);

        Task<bool> CategoryExistsById(int id);

        Task<bool> CategoryExistsByName(string name);

        Task<T> GetCategoryById<T>(int id);
    }
}
