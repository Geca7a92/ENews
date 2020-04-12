namespace ENews.Web.ViewModels.Articles
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class CategoriesDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}