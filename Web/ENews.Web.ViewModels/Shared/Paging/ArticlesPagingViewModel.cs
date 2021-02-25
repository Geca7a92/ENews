namespace ENews.Web.ViewModels.Shared.Paging
{
    using ENews.Data.Models.Enums;

    public class ArticlesPagingViewModel : PagingViewModel
    {
        public string CategoryName { get; set; }

        public string Username { get; set; }

        public string SubCategoryName { get; set; }

        public string Route { get; set; }
    }
}
