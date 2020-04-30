namespace ENews.Web.ViewModels.MembersArea.Articles
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class SubCategoriesDropDownViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}