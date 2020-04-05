﻿namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query
                = this.categoryRepository.All().OrderBy(x => x.Title);
            if (count != null)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetCategoryByName<T>(string name)
        {
            var category = this.categoryRepository.All()
                .Where(x => x.Title == name)
                .To<T>().FirstOrDefault();

            return category;
        }
    }
}