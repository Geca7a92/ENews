using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using ENews.Services.Mapping;
using ENews.Web.ViewModels.Administration.SubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public class SubCategoriesService : ISubCategoriesService
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public SubCategoriesService(IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task CreateSubCategoryAsync(SubCategoryCreateInputModel inputModel)
        {
            var subCategory = new SubCategory()
            {
                Title = inputModel.Title,
                CategoryId = inputModel.CategoryId,
            };

            await this.subCategoryRepository.AddAsync(subCategory);
            await this.subCategoryRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllWithDeletedSubCategories<T>()
        {
            IQueryable<SubCategory> query
                = this.subCategoryRepository.AllWithDeleted().OrderByDescending(c => c.CreatedOn);

            return query.To<T>().ToList();
        }
    }
}
