using ENews.Data.Models;
using ENews.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Administration.SubCategories
{
    public class IndexSubCategoryViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryTitle { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
