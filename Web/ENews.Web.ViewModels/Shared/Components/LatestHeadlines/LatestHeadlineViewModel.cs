namespace ENews.Web.ViewModels.Shared.Components.LatestHeadlines
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class LatestHeadlineViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
