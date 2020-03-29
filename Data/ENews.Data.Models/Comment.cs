namespace ENews.Data.Models
{
    using ENews.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(255)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
