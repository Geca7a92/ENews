namespace ENews.Web.ViewModels.Articles
{
    using System;

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class ArticleBaseViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryTitle { get; set; }

        public string SubCategoryTitle { get; set; }

        public string PictureImageUrl { get; set; }

        public int SeenCount { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
