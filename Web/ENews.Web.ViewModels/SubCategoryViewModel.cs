using AutoMapper;
using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels
{
    public class SubCategoryViewModel : IMapFrom<SubCategory>
    {
        public string Title { get; set; }

        public string Url => $"{this.Category.Title}/{this.Title}".Replace(' ', '-');

        public Category Category { get; set; }
    }
}
