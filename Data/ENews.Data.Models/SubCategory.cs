namespace ENews.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ENews.Data.Common.Models;

    public class SubCategory : BaseDeletableModel<int>
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
