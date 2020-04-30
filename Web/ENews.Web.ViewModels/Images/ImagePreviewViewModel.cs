namespace ENews.Web.ViewModels.Images
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class ImagePreviewViewModel : IMapFrom<Image>
    {
        public string ImageUrl { get; set; }
    }
}
