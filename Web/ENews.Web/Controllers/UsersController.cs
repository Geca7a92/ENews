namespace ENews.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ENews.Services.Data;
    using ENews.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index(string username)
        {
            var model = await this.usersService.GetUserByUsername<UserProfileViewModel>(username);

            return this.View(model);
        }
    }
}
