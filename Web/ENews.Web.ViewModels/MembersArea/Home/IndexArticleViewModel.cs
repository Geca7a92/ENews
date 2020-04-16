using ENews.Data.Models;
using ENews.Services.Mapping;
using System;

namespace ENews.Web.ViewModels.MembersArea.Home
{
    public class IndexArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PictureImageUrl { get; set; }

        public int? GalleryId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}