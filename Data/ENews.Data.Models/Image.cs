namespace ENews.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ENews.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
