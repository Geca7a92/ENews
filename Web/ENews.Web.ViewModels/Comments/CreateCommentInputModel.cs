using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENews.Web.ViewModels.Comments
{
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
