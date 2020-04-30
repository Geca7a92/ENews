namespace ENews.Web.Areas.Administration.Controllers
{
    using ENews.Common;
    using ENews.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
