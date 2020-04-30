namespace ENews.Web.ViewModels.Administration.SubCategories
{
    using System.Collections.Generic;

    public class IndexSubCategoriesViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexSubCategoryViewModel> SubCategories { get; set; }
    }
}
