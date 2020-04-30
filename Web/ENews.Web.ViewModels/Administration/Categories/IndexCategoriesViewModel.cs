namespace ENews.Web.ViewModels.Administration.Categories
{
    using System.Collections.Generic;

    public class IndexCategoriesViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
    }
}
