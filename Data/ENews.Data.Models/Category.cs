using ENews.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENews.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.SubCategories = new HashSet<SubCategory>();
            this.Articles = new HashSet<Article>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Desctription { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
