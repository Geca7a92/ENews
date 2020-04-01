using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Data.Seeding
{
    public class ArticleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
        }

        private static async Task SeedArticleAsync(ApplicationDbContext dbContext, string title, string content, string pictureUrl)
        {
        }
    }
}

//public int PictureId { get; set; }

//public Image Picture { get; set; }

//public int? GalleryId { get; set; }

//public Gallery Gallery { get; set; }

//[Required]
//public string AuthorId { get; set; }

//public virtual ApplicationUser Author { get; set; }

//public virtual ICollection<ArticleSubCategory> SubCategories { get; set; }

//public virtual ICollection<ArticleCategory> Categories { get; set; }

//public ICollection<Comment> Comments { get; set; }

//public Area? Area { get; set; }
