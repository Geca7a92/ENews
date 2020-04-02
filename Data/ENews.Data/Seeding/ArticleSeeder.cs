using ENews.Data.Models;
using ENews.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Data.Seeding
{
    public class ArticleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Articles.Any())
            {
                await SeedArticleAsync(dbContext, "Dogs", "Content with CuteDogs");
                await SeedArticleAsync(dbContext, "Mooncake", "Content for Mooncake");
                await SeedArticleAsync(dbContext, "Boat", "Content for Boat");
            }
        }

        private static async Task SeedArticleAsync(ApplicationDbContext dbContext, string title, string content)
        {
            var article = dbContext.Articles.FirstOrDefault(c => c.Title == title);
            if (article == null)
            {
                var adminId = dbContext.Users.FirstOrDefault().Id;
                var picture = dbContext.Images.FirstOrDefault(p => p.Description == title).Id;
                var newArticle = await dbContext.Articles.AddAsync(new Article
                {
                    Title = title,
                    Content = content,
                    AuthorId = adminId,
                    PictureId = picture,
                    Area = Models.Enums.Area.Sofia,
                });
                newArticle.Entity.Categories.Add(new ArticleCategory
                {
                    ArticleId = newArticle.Entity.Id,
                    CategoryId = 6,
                });

                newArticle.Entity.SubCategories.Add(new ArticleSubCategory
                {
                    ArticleId = newArticle.Entity.Id,
                    SubCategoryId = 1,
                });
                dbContext.SaveChanges();
            }
        }
    }
}
