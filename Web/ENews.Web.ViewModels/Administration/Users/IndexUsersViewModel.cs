using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Administration.Users
{
    public class IndexUsersViewModel
    {
        public IEnumerable<IndexUserViewModel> Users { get; set; }
    }
}
