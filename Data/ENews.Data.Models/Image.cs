using ENews.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENews.Data.Models
{
    public class Image : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        //public int? GalleryId { get; set; }

        //public virtual Gallery Gallery { get; set; }

        //public int? CategoryId { get; set; }

        //public virtual Category Category { get; set; }

        //public int? UserId { get; set; }

        //public ApplicationUser ApplicationUser { get; set; }
    }
}
