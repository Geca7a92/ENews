using ENews.Common;
using ENews.Data;
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
        private readonly IUsersService usersService;

        public UsersController(
            ApplicationDbContext context,
            IUsersService usersService)
        {
            this.context = context;
            this.usersService = usersService;
        }

        public IActionResult Banned()
        {
            var result = this.usersService.GetAllBanned();
            var model = new IndexUsersViewModel()
            {
                Users = result,
            };

            return this.View(model);
        }

        public IActionResult Active()
        {
            var result = this.usersService.GetAll();
            var model = new IndexUsersViewModel()
            {
                Users = result,
            };

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
