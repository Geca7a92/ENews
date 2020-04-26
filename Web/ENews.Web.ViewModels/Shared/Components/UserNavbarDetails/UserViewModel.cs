using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Shared.Components.UserNavbarDetails
{
    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string ProfilePictureImageUrl { get; set; }
    }
}
