namespace ENews.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;

    public class IndexUsersViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<IndexUserViewModel> Users { get; set; }
    }
}
