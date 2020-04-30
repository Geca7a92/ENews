namespace ENews.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ENews.Data.Models.Enums;

    public class Address
    {
        public int Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public Region? Region { get; set; }

        [MaxLength(50)]
        public string City { get; set; }
    }
}
