namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Mapping;
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

        public T GetSubCategoryByName<T>(string name)
        {
            var category = this.subCategoryRepository.All()
                .Where(x => x.Title == name)
                .To<T>().FirstOrDefault();

            return category;
        }
    }
}
