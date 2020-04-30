namespace ENews.Web.ViewModels
{
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class SubCategoryViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url => $"{this.Category.Title}/{this.Title}".Replace(' ', '-');

        public Category Category { get; set; }
    }
}
