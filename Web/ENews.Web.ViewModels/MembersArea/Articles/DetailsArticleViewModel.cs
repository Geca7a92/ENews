namespace ENews.Web.ViewModels.MembersArea.Articles
{
    using System;

    using ENews.Data.Models;
    using ENews.Data.Models.Enums;
    using ENews.Services.Mapping;

    public class DetailsArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int SeenCount { get; set; }

        public string VideoUrl { get; set; }

        public string PictureImageUrl { get; set; }

        public int? GalleryId { get; set; }

        public string AuthorUserName { get; set; }

        public string SubCategoryTitle { get; set; }

        public string CategoryTitle { get; set; }

        public int CommentsCount { get; set; }

        public Region? Region { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
