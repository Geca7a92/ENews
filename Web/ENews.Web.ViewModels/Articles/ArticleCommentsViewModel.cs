namespace ENews.Web.ViewModels.Articles
{
    using System;

    using AutoMapper;
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class ArticleCommentsViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string UserUserName { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserProfilePictureImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, ArticleCommentsViewModel>();
        }
    }
}