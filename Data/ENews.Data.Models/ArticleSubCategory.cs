using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Data.Models
{
    public class ArticleSubCategory
    {
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }
    }
}
