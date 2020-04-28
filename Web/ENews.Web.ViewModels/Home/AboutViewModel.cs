using ENews.Web.ViewModels.Administration.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Web.ViewModels.Home
{
    public class AboutViewModel
    {
        public int ArticlesCount { get; set; }

        public int MembersCount { get; set; }

        public int UsersCount { get; set; }

        public int ArticleReads { get; set; }

        public IEnumerable<EmployeeViewModel> TopEmployees { get; set; }
    }
}
