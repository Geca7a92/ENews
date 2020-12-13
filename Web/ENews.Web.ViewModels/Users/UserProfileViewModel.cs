﻿namespace ENews.Web.ViewModels.Users
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class UserProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public bool MyProfile { get; set; } = false;

        public string ProfilePictureImageUrl { get; set; }
    }
}