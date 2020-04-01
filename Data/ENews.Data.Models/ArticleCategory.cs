using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ENews.Data.Models
{
    public class ArticleCategory
    {
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
