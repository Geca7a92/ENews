using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Administration.Users
{
    public class IndexUsersViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexUserViewModel> Users { get; set; }
    }
}
