namespace ENews.Web.ViewModels.Home
{

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class EmployeeViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string ProfilePictureImageUrl { get; set; }
    }
}
