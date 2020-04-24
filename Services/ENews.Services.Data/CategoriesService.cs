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

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<T> GetAllCategories<T>()
        {
            IQueryable<Category> query
                = this.categoryRepository.All().OrderByDescending(x => x.CreatedOn);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllDeletedCategories<T>()
        {
            IQueryable<Category> query
                = this.categoryRepository.AllWithDeleted().Where(c => c.IsDeleted).OrderByDescending(x => x.CreatedOn);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllWithDeletedCategories<T>()
        {
            IQueryable<Category> query
                = this.categoryRepository.AllWithDeleted().OrderByDescending(c => c.CreatedOn);

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

        public IEnumerable<T> GetAllCategoriesWithAnySubCategories<T>()
        {
            IQueryable<Category> query
                = this.categoryRepository.All().OrderBy(x => x.Title).Where(c => c.SubCategories.Any());

            return query.To<T>().ToList();
        }

        public async Task<int> UpdateCategory(Category category)
        {
            this.categoryRepository.Update(category);
            return await this.categoryRepository.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryModelById(int id)
        {
            return await this.categoryRepository.GetByIdWithDeletedAsync(id);
        }
    }
}
