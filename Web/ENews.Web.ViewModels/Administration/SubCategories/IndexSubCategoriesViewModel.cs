namespace ENews.Web.ViewModels.Administration.SubCategories
{
    using System.Collections.Generic;

    using ENews.Web.ViewModels.Shared.Paging;

    public class IndexSubCategoriesViewModel : PagingViewModel
    {
        public IEnumerable<IndexSubCategoryViewModel> SubCategories { get; set; }
    }
}
