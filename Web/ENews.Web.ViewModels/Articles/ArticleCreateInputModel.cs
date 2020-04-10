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

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public ICollection<IFormFile> GalleryContent { get; set; }

        [Required]
        public IFormFile MainImage { get; set; }
    }
}
