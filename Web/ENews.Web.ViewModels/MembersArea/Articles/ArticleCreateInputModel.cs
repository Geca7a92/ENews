using ENews.Data.Models;
using ENews.Services.Mapping;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ENews.Web.ViewModels.MembersArea.Articles
{
    public class ArticleCreateInputModel : IMapFrom<Article>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000, ErrorMessage = "To long")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoriesDropDownViewModel> CategoriesDropDown { get; set; }

        [Required]
        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }

        [Display(Name = "Gallery content")]
        public ICollection<IFormFile> GalleryContent { get; set; }

        [Required]
        [Display(Name = "Main image")]
        public IFormFile MainImage { get; set; }
    }
}
