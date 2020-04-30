namespace ENews.Web.ViewModels.Administration.Categories
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class DropDownCategories : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
