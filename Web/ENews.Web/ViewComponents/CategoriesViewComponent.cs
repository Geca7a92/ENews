namespace ENews.Web.ViewComponents
{
    using System.Linq;

    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Data;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Shared.Components;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesViewComponent(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new CategoriesViewModel
            {
                Categories = this.categoriesService.GetAllCategories<CategoryViewModel>(),
            };

            return this.View(model);
        }
    }
}
