using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Administration.Categories
{
    public class IndexCategoriesViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
    }
}
