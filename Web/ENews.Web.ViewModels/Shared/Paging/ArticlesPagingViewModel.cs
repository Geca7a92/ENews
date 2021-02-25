namespace ENews.Web.ViewModels.Shared.Paging
{
    using ENews.Data.Models.Enums;

    public class ArticlesPagingViewModel : PagingViewModel
    {
        public string Data { get; set; }

        public string SubData { get; set; }

        public string Route { get; set; }
    }
}
