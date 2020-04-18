using ENews.Common;
using ENews.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Data.Seeding
{
    public class ArticleSeeder : ISeeder
    {
        public const string TestString = "Historically certain manufacturing industries have gone into a decline due to various economic factors, including the development of replacement technology or the loss of competitive advantage. " +
            "An example of the former is the decline in carriage manufacturing when the automobile was mass-produced.A recent trend has been the migration of prosperous, industrialized nations towards a post-industrial society. " +
            "This is manifested by an increase in the service sector at the expense of manufacturing, and the development of an information-based economy, the so-called informational revolution. " +
            "In a post-industrial society, manufacturers relocate to more profitable locations through a process of off-shoring.Measurements of manufacturing industries outputs and economic effect are not historically stable. " +
            "Traditionally, success has been measured in the number of jobs created. " +
            "The reduced number of employees in the manufacturing sector has been assumed to result from a decline in the competitiveness of the sector, or the introduction of the lean manufacturing process. " +
            "Related to this change is the upgrading of the quality of the product being manufactured. " +
            "While it is possible to produce a low-technology product with low-skill labour, the ability to manufacture high-technology products well is dependent on a highly skilled staff.";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var adminRoleId = dbContext.Roles.FirstOrDefault(c => c.Name == GlobalConstants.AdministratorRoleName).Id;
            var userId = dbContext.Users.Where(u => u.Roles.Any(r => r.RoleId == adminRoleId)).FirstOrDefault().Id;
            if (dbContext.Articles.Count() < 3)
            {
                await SeedArticleAsync(dbContext, "Some Test Name", TestString, userId, 2, 1, 18);
                await SeedArticleAsync(dbContext, "Some Test Name Two", TestString, userId, 3, 1, 18);
                await SeedArticleAsync(dbContext, "Some Test Name AnotherOne", TestString, userId, 1, 1, 19);
            }
        }

        private static async Task SeedArticleAsync(ApplicationDbContext dbContext, string articleName, string content, string authorId, int picureId, int categoryId, int subCategoryId)
        {
            await dbContext.Articles.AddAsync(new Article
            {
                Title = articleName,
                Content = content,
                AuthorId = authorId,
                PictureId = picureId,
                CategoryId = categoryId,
                SubCategoryId = subCategoryId,
            });
        }
    }
}