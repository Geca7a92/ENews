namespace ENews.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ENews.Data;
    using ENews.Data.Models;
    using ENews.Data.Models.Enums;
    using ENews.Services.Data;
    using ENews.Web.ViewModels;
    using ENews.Web.ViewModels.Home;
    using ENews.Web.ViewModels.Shared.Components.GetLocalLastFiveArticles;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class GetLocalLastFiveArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articleService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAddressesService addressesService;

        public GetLocalLastFiveArticlesViewComponent(
            IArticlesService articleService,
            UserManager<ApplicationUser> userManager,
            IAddressesService addressesService)
        {
            this.articleService = articleService;
            this.userManager = userManager;
            this.addressesService = addressesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new LocalLastFiveArticlesViewModel();
            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (currentUser == null)
            {
                model.Articles = this.articleService.GetLatesLocalArticles<ArticlePreviewViewModel>();
            }
            else if (currentUser.AddressId != null)
            {
                var addres = this.addressesService.GetAddresById((int)currentUser.AddressId);

                if (addres.Region == null)
                {
                    model.Articles = this.articleService.GetLatesLocalArticles<ArticlePreviewViewModel>();
                }
                else
                {
                    model.Region = addres.Region;
                    model.Articles = this.articleService.GetLatestByRegion<ArticlePreviewViewModel>((Region)addres.Region);
                }
            }
            else
            {
                model.Articles = this.articleService.GetLatesLocalArticles<ArticlePreviewViewModel>();
            }

            return this.View(model);
        }
    }
}
