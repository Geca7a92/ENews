using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Articles
{
    public class ArticleCommentsViewModel : IMapFrom<Comment>
    {
        public string Content { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}