namespace ENews.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;

    using ENews.Web.ViewModels.Shared.Paging;

    public class IndexUsersViewModel : PagingViewModel
    {
        public IEnumerable<IndexUserViewModel> Users { get; set; }
    }
}
