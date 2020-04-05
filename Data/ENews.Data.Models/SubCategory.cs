using ENews.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENews.Data.Models
{
    public class SubCategory : BaseModel<int>
    {
        public SubCategory()
        {
            this.Articles = new HashSet<Article>();
        }

        public string Title { get; set; }

        public Category Category { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
