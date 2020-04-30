namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Administration.SubCategories;
    using Microsoft.EntityFrameworkCore;

    public class SubCategoriesService : ISubCategoriesService
    {
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public SubCategoriesService(IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        public IEnumerable<T> GetAllSubCategories<T>(int? take = null, int skip = 0)
        {
            IQueryable<SubCategory> query
                = this.subCategoryRepository.All().OrderByDescending(x => x.CreatedOn).Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllDeletedSubCategories<T>(int? take = null, int skip = 0)
        {
            IQueryable<SubCategory> query
                = this.subCategoryRepository.AllWithDeleted().Where(c => c.IsDeleted).OrderByDescending(x => x.CreatedOn).Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
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

        public async Task DeleteById(int id)
        {
            var category = await this.subCategoryRepository.GetByIdWithDeletedAsync(id);
            this.subCategoryRepository.Delete(category);
            await this.subCategoryRepository.SaveChangesAsync();
        }

        public async Task UndeleteById(int id)
        {
            var category = await this.subCategoryRepository.GetByIdWithDeletedAsync(id);
            this.subCategoryRepository.Undelete(category);
            await this.subCategoryRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetSubCategoriesOfCategoryId<T>(int id)
        {
            IQueryable<SubCategory> query
                = this.subCategoryRepository.All().OrderBy(x => x.Title).Where(x => x.CategoryId == id);

            return query.To<T>().ToList();
        }

        public async Task<T> GetSubCategoryById<T>(int id)
        {
            var subCategory = await this.subCategoryRepository.AllWithDeleted().Where(c => c.Id == id)
                .To<T>().FirstOrDefaultAsync();
            return subCategory;
        }

        public T GetSubCategoryByName<T>(string name)
        {
            var category = this.subCategoryRepository.All()
                .Where(x => x.Title == name)
                .To<T>().FirstOrDefault();

            return category;
        }

        public async Task HardDeleteById(int id)
        {
            var category = await this.subCategoryRepository.GetByIdWithDeletedAsync(id);
            this.subCategoryRepository.HardDelete(category);
            await this.subCategoryRepository.SaveChangesAsync();
        }

        public async Task<bool> SubCategoryExistsById(int id)
        {
            var category = await this.subCategoryRepository.GetByIdWithDeletedAsync(id);
            if (category == null || category.IsDeleted)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SubCategoryExistsByName(string name, int? id = null)
        {
            if (id == null)
            {
                return await this.subCategoryRepository.AllWithDeleted().AnyAsync(c => c.Title.ToLower() == name.ToLower());
            }

            return await this.subCategoryRepository.AllWithDeleted().Where(sc => sc.CategoryId == id).AnyAsync(c => c.Title.ToLower() == name.ToLower());
        }

        public async Task<int> Update(SubCategory subCategory)
        {
            this.subCategoryRepository.Update(subCategory);
            return await this.subCategoryRepository.SaveChangesAsync();
        }

        public async Task<SubCategory> GetSubCategoryModelById(int id)
        {
            return await this.subCategoryRepository.GetByIdWithDeletedAsync(id);
        }

        public int GetCountOfActiveSubCategories()
        {
            return this.subCategoryRepository.All().Count();
        }

        public int GetCountOfDeletedSubCategories()
        {
            return this.subCategoryRepository.AllWithDeleted().Where(u => u.IsDeleted).Count();
        }
    }
}
