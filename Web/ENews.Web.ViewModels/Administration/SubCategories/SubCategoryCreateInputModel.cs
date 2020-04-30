namespace ENews.Web.ViewModels.Administration.SubCategories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.MembersArea.Articles;

    public class SubCategoryCreateInputModel : IMapFrom<SubCategory>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoriesDropDownViewModel> CategoriesDropDown { get; set; }
    }
}
