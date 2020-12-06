namespace ENews.Web.ViewModels.Administration.Categories
{
    using System.Collections.Generic;

    using ENews.Web.ViewModels.Shared.Paging;

    public class IndexCategoriesViewModel : PagingViewModel
    {
        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
    }
}
