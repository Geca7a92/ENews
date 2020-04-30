namespace ENews.Web.ViewModels.Shared.Components.UserNavbarDetails
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string ProfilePictureImageUrl { get; set; }
    }
}
