using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Administration.Categories
{
   public class DropDownCategories : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
