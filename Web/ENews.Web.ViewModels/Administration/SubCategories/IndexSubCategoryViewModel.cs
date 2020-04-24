namespace ENews.Web.ViewModels.Administration.SubCategories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class IndexSubCategoryViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Main category")]
        public string CategoryTitle { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
