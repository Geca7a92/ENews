namespace ENews.Web.ViewModels.MembersArea.Articles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.Infrastructure.ValidationAttributes;
    using Microsoft.AspNetCore.Http;

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
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [MaxFileSize(1024 * 1024)]
        public ICollection<IFormFile> GalleryContent { get; set; }

        [Required]
        [Display(Name = "Main image")]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [MaxFileSize(1024 * 1024)]
        public IFormFile MainImage { get; set; }
    }
}
