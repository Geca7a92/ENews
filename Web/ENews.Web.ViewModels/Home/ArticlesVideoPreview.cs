namespace ENews.Web.ViewModels.Home
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class ArticlesVideoPreview : IMapFrom<Article>
    {
        public string Title { get; set; }

        public string VideoUrl { get; set; }

        public string PictureImageUrl { get; set; }
    }
}
