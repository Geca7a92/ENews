using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ENews.Web.ViewModels.Articles
{
    public class ArticleCreateInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

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
