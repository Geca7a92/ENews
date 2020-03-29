using ENews.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENews.Data.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public Area? Area { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(150)]
        public string AddressText { get; set; }
    }
}
