namespace ENews.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data;
    using ENews.Services;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Administration.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class UsersController : BaseAdminController
    {
        private readonly IPagingService pagingService;
        private readonly IUsersService usersService;

        public UsersController(
            IPagingService pagingService,
            IUsersService usersService)
        {
            this.pagingService = pagingService;
            this.usersService = usersService;
        }

        public IActionResult Banned(string sortBy, string search, int page = 1)
        {
            this.SortBy(sortBy);
            this.Search(search);

            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var result = this.usersService.GetAllBanned(sortBy, search, GlobalConstants.AdministrationItemsPerPage, skip);

            var usersCount = this.usersService.GetCountofBannedUsersByName(search);

            var model = new IndexUsersViewModel()
            {
                Users = result,
                PagesCount = this.pagingService.PagesCount(usersCount, GlobalConstants.AdministrationItemsPerPage),
            };
            model.CurrentPage = this.pagingService.PagesCount(page, model.PagesCount);

            return this.View(model);
        }

        public IActionResult Active(string sortBy, string search, int page = 1)
        {
            this.SortBy(sortBy);
            this.Search(search);

            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var result = this.usersService.GetAll(sortBy, search, GlobalConstants.AdministrationItemsPerPage, skip);

            var usersCount = this.usersService.GetCountofActiveAccountsByUsername(search);

            var model = new IndexUsersViewModel()
            {
                Users = result,
                PagesCount = this.pagingService.PagesCount(usersCount, GlobalConstants.AdministrationItemsPerPage),
            };
            model.CurrentPage = this.pagingService.SetPage(page, model.PagesCount);

            return this.View(model);
        }

        public async Task<IActionResult> Ban(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.usersService.DeleteById(id);

            return this.RedirectToAction(nameof(this.Active));
        }

        public async Task<IActionResult> Unban(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.usersService.UndeleteById(id);

            return this.RedirectToAction(nameof(this.Banned));
        }

        public async Task<IActionResult> AddRoleReporter(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.usersService.AddReporterRoleToUser(id);
            return this.RedirectToAction(nameof(this.Active));

        }

        public async Task<IActionResult> RemoveRoleReporter(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            await this.usersService.RemoveRepoertRoleFromUser(id);
            return this.RedirectToAction(nameof(this.Active));
        }

        public IActionResult GetReporters()
        {
            var res = this.usersService.GetAllReporters();
            var test = Newtonsoft.Json.JsonConvert.SerializeObject(res);
            return this.Json(res);
        }
    }
}
