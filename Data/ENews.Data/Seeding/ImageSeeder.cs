using ENews.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Data.Seeding
{
    public class ImageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Images.Any())
            {
                var images = new List<Image>
                {
                    new Image
                    {
                        Description = "Mooncake",
                        ImageUrl = @"https://res.cloudinary.com/dijwyj1gn/image/upload/v1583847993/i7strmimssiml963zqf9.png",
                    },
                    new Image
                    {
                        Description = "Dogs",
                        ImageUrl = @"https://res.cloudinary.com/dijwyj1gn/image/upload/v1583842407/samples/animals/three-dogs.jpg",
                    },
                    new Image
                    {
                        Description = "Boat",
                        ImageUrl = @"https://res.cloudinary.com/dijwyj1gn/image/upload/v1583842408/samples/landscapes/beach-boat.jpg",
                    },
                };
                await dbContext.Images.AddRangeAsync(images);
            }
        }
    }
}
