namespace ENews.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class AboutViewModel
    {
        public int ArticlesCount { get; set; }

        public int MembersCount { get; set; }

        public int UsersCount { get; set; }

        public int ArticleReads { get; set; }

        public IEnumerable<EmployeeViewModel> TopEmployees { get; set; }
    }
}
