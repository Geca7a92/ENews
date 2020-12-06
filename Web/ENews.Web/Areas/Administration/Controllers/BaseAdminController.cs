namespace ENews.Web.Areas.Administration.Controllers
{
    using ENews.Common;
    using Microsoft.AspNetCore.Mvc;

    public class BaseAdminController : Controller
    {
        protected void SortBy(string sortBy)
        {
            var emailSort = sortBy == GlobalConstants.EmailSortTypeAsc ? GlobalConstants.EmailSortTypeDesc : GlobalConstants.EmailSortTypeAsc;
            var craetedOnSort = sortBy == GlobalConstants.CreatedOnTypeAsc ? GlobalConstants.CreatedTypeOnDesc : GlobalConstants.CreatedOnTypeAsc;
            var usernameSort = sortBy == GlobalConstants.UsernameTypeAsc ? GlobalConstants.UsernamepeOnDesc : GlobalConstants.UsernameTypeAsc;

            this.ViewData[GlobalConstants.EmailSortBtn] = emailSort;

            this.ViewData[GlobalConstants.CreatedOnSortBtn] = craetedOnSort;

            this.ViewData[GlobalConstants.UsernameSortBtn] = usernameSort;

            this.ViewData["currentSortOrder"] = sortBy;
        }

        protected void Search(string search)
        {
            this.ViewData["search"] = search;
        }
    }
}
