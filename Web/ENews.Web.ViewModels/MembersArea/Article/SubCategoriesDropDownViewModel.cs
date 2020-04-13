using ENews.Data.Models;
using ENews.Services.Mapping;

namespace ENews.Web.ViewModels.MembersArea.Article
{
    public class SubCategoriesDropDownViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}