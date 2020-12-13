namespace ENews.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ENews.Data.Models;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string username)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var model = await this.usersService.GetUserByUsername<UserProfileViewModel>(username);
            if (user != null)
            {
                model.MyProfile = user.Id == model.Id ? true : false;
            }

            return this.View(model);
        }
    }
}
