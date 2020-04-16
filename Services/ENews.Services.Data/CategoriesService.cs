namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Administration.Categories;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository, IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public IEnumerable<T> GetAllCategories<T>()
        {
            IQueryable<Category> query
                = this.categoryRepository.All().OrderBy(x => x.Title);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllWithDeletedCategories<T>()
        {
            IQueryable<Category> query
                = this.categoryRepository.AllWithDeleted().OrderByDescending(c => c.CreatedOn);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetSubCategoriesOfCategoryId<T>(int id)
        {
            IQueryable<SubCategory> query
                = this.subCategoryRepository.All().OrderBy(x => x.Title).Where(x => x.CategoryId == id);

            return query.To<T>().ToList();
        }

        public T GetCategoryByName<T>(string name)
        {
            var category = this.categoryRepository.All()
                .Where(x => x.Title == name)
                .To<T>().FirstOrDefault();

            return category;
        }

        public async Task<T> GetCategoryById<T>(int id)
        {
            var category = await this.categoryRepository.AllWithDeleted().Where(c => c.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return category;
        }

        public T GetSubCategoryByName<T>(string name)
        {
            var category = this.subCategoryRepository.All()
                .Where(x => x.Title == name)
                .To<T>().FirstOrDefault();

            return category;
        }

        public async Task CreateCategoryAsync(CategoryCreateInputModel inputModel)
        {
            var category = new Category()
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task HardDeleteById(int id)
        {
            var category = await this.categoryRepository.GetByIdWithDeletedAsync(id);
            this.categoryRepository.HardDelete(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var category = await this.categoryRepository.GetByIdWithDeletedAsync(id);
            this.categoryRepository.Delete(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task UndeleteById(int id)
        {
            var category = await this.categoryRepository.GetByIdWithDeletedAsync(id);
            this.categoryRepository.Undelete(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsById(int id)
        {
            var category = await this.categoryRepository.GetByIdWithDeletedAsync(id);
            if (category == null || category.IsDeleted)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CategoryExistsByName(string name)
        {
            return await this.categoryRepository.AllWithDeleted().AnyAsync(c => c.Title.ToLower() == name.ToLower());
        }
    }
}
