using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Administration.SubCategories
{
    public class IndexSubCategoriesViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexSubCategoryViewModel> SubCategories { get; set; }
    }
}
