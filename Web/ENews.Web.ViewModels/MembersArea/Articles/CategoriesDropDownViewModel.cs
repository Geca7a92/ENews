namespace ENews.Web.ViewModels.MembersArea.Articles
{
    using System.Collections.Generic;

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class CategoriesDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual IEnumerable<SubCategory> SubCategories { get; set; }
    }
}