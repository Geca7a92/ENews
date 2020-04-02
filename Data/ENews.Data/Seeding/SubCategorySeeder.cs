using ENews.Common;
using ENews.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Data.Seeding
{
    public class SubCategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.SubCategories.Any())
            {
                await SeedSubCategoryAsync(dbContext, "Business", GlobalConstants.BussinesSubcategories);
                await SeedSubCategoryAsync(dbContext, "Politics", GlobalConstants.PoliticsSubcategories);
                await SeedSubCategoryAsync(dbContext, "World", GlobalConstants.WorldSubcategories);
                await SeedSubCategoryAsync(dbContext, "Society", GlobalConstants.SocietySubcategories);
                await SeedSubCategoryAsync(dbContext, "Sport", GlobalConstants.SportSubcategories);
                await SeedSubCategoryAsync(dbContext, "Culture", GlobalConstants.CultureSubcategories);
            }
        }

        private static async Task SeedSubCategoryAsync(ApplicationDbContext dbContext, string categoryName, ICollection<string> subCategoryNames)
        {
            var category = dbContext.Categories.FirstOrDefault(c => c.Title == categoryName);
            if (category != null)
            {
                foreach (var subCategoryName in subCategoryNames)
                {
                    if (!dbContext.SubCategories.Any(s => s.Title == subCategoryName))
                    {
                        await dbContext.SubCategories.AddAsync(new SubCategory
                        {
                            Title = subCategoryName,
                            CategoryId = category.Id,
                        });
                    }
                }
            }
        }
    }
}
