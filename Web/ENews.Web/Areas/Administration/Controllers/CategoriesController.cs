using ENews.Common;
using ENews.Data;
using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using ENews.Web.ViewModels.Administration.Categories;
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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesController(ApplicationDbContext context, IDeletableEntityRepository<Category> categoryRepository)
        {
            this.context = context;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var res = await this.context.Categories.ToListAsync();
            return this.View(res);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var res = await this.categoryRepository.GetByIdWithDeletedAsync(id);

            if (res == null)
            {
                return this.NotFound();
            }
            return this.View(res);
        }
    }
}
