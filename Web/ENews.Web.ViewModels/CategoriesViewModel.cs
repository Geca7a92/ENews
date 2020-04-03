using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels
{
    public class CategoriesViewModel
    {
        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
