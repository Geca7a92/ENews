using ENews.Common;
using ENews.Data;
using ENews.Services;
using ENews.Services.Data;
using ENews.Web.ViewModels.Administration.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENews.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IPagingService pagingService;
        private readonly IUsersService usersService;

        public UsersController(
            ApplicationDbContext context,
            IPagingService pagingService,
            IUsersService usersService)
        {
            this.context = context;
            this.pagingService = pagingService;
            this.usersService = usersService;
        }

        public IActionResult Banned(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var result = this.usersService.GetAllBanned(GlobalConstants.AdministrationItemsPerPage, skip);

            var usersCount = this.usersService.GetCountofBannedMembersAndUsers();

            var model = new IndexUsersViewModel()
            {
                Users = result,
                PagesCount = this.pagingService.PagesCount(usersCount, GlobalConstants.AdministrationItemsPerPage),
            };
            model.CurrentPage = this.pagingService.PagesCount(page, model.PagesCount);

            return this.View(model);
        }

        public IActionResult Active(int page = 1)
        {
            var skip = this.pagingService.CountSkip(page, GlobalConstants.AdministrationItemsPerPage);

            var result = this.usersService.GetAll(GlobalConstants.AdministrationItemsPerPage, skip);

            var usersCount = this.usersService.GetCountofActiveMembersAndUsers();

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
    }
}
