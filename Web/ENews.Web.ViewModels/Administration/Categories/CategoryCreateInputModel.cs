namespace ENews.Web.ViewModels.Administration.Categories
{
    using System.ComponentModel.DataAnnotations;

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class CategoryCreateInputModel : IMapFrom<Category>
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
