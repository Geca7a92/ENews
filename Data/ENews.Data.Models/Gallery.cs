namespace ENews.Data.Models
{
    using System.Collections.Generic;

    using ENews.Data.Common.Models;

    public class Gallery : BaseDeletableModel<int>
    {
        public Gallery()
        {
            this.Images = new HashSet<Image>();
        }

        public Article Article { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
