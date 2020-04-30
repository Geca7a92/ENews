namespace ENews.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class CreateCommentInputModel : IMapFrom<Comment>
    {
        public int ArticleId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Comment")]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
