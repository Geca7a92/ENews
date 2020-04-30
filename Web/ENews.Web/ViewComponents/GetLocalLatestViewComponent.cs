namespace ENews.Web.ViewComponents
{
    using System.Threading.Tasks;

    using ENews.Data.Models;
    using ENews.Data.Models.Enums;
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Shared.Components.GetLocalLatest;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class GetLocalLatestViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAddressesService addressesService;

        public GetLocalLatestViewComponent(
            IArticlesService articleService,
            UserManager<ApplicationUser> userManager,
            IAddressesService addressesService)
        {
            this.articleService = articleService;
            this.userManager = userManager;
            this.addressesService = addressesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var model = new LocalLatestArticlesViewModel();
            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (currentUser == null)
            {
                model.Articles = this.articleService.GetLatesLocalArticles<ArticlePreviewViewModel>(count);
            }
            else if (currentUser.AddressId != null)
            {
                var addres = this.addressesService.GetAddresById((int)currentUser.AddressId);

                if (addres.Region == null)
                {
                    model.Articles = this.articleService.GetLatesLocalArticles<ArticlePreviewViewModel>(count);
                }
                else
                {
                    model.Region = addres.Region;
                    model.Articles = this.articleService.GetLatestByRegion<ArticlePreviewViewModel>((Region)addres.Region, count);
                }
            }
            else
            {
                model.Articles = this.articleService.GetLatesLocalArticles<ArticlePreviewViewModel>(count);
            }

            return this.View(model);
        }
    }
}
