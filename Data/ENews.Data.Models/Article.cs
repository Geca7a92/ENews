namespace ENews.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ENews.Data.Common.Models;
    using ENews.Data.Models.Enums;

    public class Article : BaseDeletableModel<int>
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        // ToDo
        // public int SeenCount { get; set; }
        public string VideoUrl { get; set; }

        public int PictureId { get; set; }

        public Image Picture { get; set; }

        public int? GalleryId { get; set; }

        public Gallery Gallery { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Region? Region { get; set; }
    }
}
