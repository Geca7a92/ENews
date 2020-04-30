namespace ENews.Web.ViewComponents
{
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Shared.Components.Categories;
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
