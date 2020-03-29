using ENews.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Data.Models
{
    public class Gallery : BaseDeletableModel<int>
    {
        public Gallery()
        {
            this.Images = new HashSet<Image>();
        }

        public string Description { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
