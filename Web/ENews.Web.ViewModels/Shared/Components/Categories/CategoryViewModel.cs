namespace ENews.Web.ViewModels.Shared.Components.Categories
{
    using System.Collections.Generic;

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }
    }
}