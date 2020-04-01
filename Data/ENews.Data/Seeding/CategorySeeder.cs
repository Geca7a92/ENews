using ENews.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Data.Seeding
{
    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedCategoryAsync(dbContext, "Business");
            await SeedCategoryAsync(dbContext, "Politics");
            await SeedCategoryAsync(dbContext, "World");
            await SeedCategoryAsync(dbContext, "Society");
            await SeedCategoryAsync(dbContext, "Sport");
            await SeedCategoryAsync(dbContext, "Culture");
            await SeedCategoryAsync(dbContext, "Bulgaria");
        }

        private static async Task SeedCategoryAsync(ApplicationDbContext dbContext, string categotyName)
        {
            var category = dbContext.Categories.FirstOrDefault(x => x.Title == categotyName);
            if (category == null)
            {
                var result = await dbContext.Categories.AddAsync(new Category
                {
                    Title = categotyName,
                    Desctription = categotyName,
                });
            }
        }
    }
}
