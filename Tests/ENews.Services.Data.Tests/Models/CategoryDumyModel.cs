namespace ENews.Services.Data.Tests.Models
{
    using System;
    using System.Collections.Generic;

    using ENews.Data.Models;
    using ENews.Services.Mapping;

    public class CategoryDumyModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<SubCategory> SubCategories { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
