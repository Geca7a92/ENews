using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENews.Web.ViewModels.Administration.Categories
{
    public class CategoryCreateInputModel : IMapFrom<Category>
    {
        [Required]
        [MaxLength(50)]

        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
