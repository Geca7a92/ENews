namespace ENews.Web.ViewComponents
{
    using System.Linq;

    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesViewComponent(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = new CategoriesViewModel
            {
                Categories = this.categoryRepository.All()
                .OrderBy(c => c.Title).To<CategoryViewModel>().ToList(),
            };

            return this.View(model);
        }
    }
}
