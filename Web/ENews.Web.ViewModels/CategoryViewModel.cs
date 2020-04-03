using ENews.Data.Models;
using ENews.Services.Mapping;
using System.Collections.Generic;

namespace ENews.Web.ViewModels
{
    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }
    }
}