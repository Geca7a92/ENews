using ENews.Data.Models;
using ENews.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ENews.Web.ViewModels.Administration.Users
{
    public class IndexUserViewModel : IMapFrom<ApplicationUser>
    {
        public IndexUserViewModel()
        {
            this.RolesNames = new List<string>();
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual IEnumerable<IdentityUserRole<string>> Roles { get; set; }

        [Display(Name = "Roles")]
        public List<string> RolesNames { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Is banned")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Banned on")]
        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
