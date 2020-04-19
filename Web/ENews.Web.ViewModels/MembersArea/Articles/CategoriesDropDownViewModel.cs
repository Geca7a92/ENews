namespace ENews.Web.ViewModels.MembersArea.Articles
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using System.Collections.Generic;

    public class CategoriesDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual IEnumerable<SubCategory> SubCategories { get; set; }
    }
}