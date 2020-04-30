namespace ENews.Web.ViewComponents
{
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data.Models;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Shared.Components.UserNavbarDetails;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    // Not Used
    public class UserNavbarDetailsViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IImagesService imagesService;

        public UserNavbarDetailsViewComponent(
            UserManager<ApplicationUser> userManager,
            IImagesService imagesService)
        {
            this.userManager = userManager;
            this.imagesService = imagesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);

            var pictureUrl = currentUser.ProfilePictureId != null ? this.imagesService.GetImageById((int)currentUser.ProfilePictureId).ImageUrl : GlobalConstants.ProfilePictureFill;
            var model = new UserViewModel()
            {
                ProfilePictureImageUrl = pictureUrl,
            };
            return this.View(model);
        }
    }
}
