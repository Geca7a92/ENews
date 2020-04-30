using ENews.Data.Models;
using ENews.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Services.Data.Tests.Seed
{
    public class SeedArticle
    {
        public static Article Create()
        {
            return new Article
            {
                Category = new Category { Title = "asd", Id = 1 },
                SubCategory = new SubCategory { Title = "asd", Id = 1 },
                Content = "Content1",
                Title = "Title1",
                PictureId = 1,
                SeenCount = 1,
                Id = 1,
                Author = new ApplicationUser { FirstName = "asd", LastName = "asd", Id = "ads", CreatedOn = DateTime.Now, ProfilePicture = new Image { ImageUrl = "aaaa" } },
                Picture = new Image { ImageUrl = "testurl" },
                CreatedOn = DateTime.Now,
                Comments = new List<Comment>(),
                Region = null,
                VideoUrl="videoUrl",
            };
        }

        public static Article CreateSecondArticle()
        {
            return new Article
            {
                Category = new Category { Title = "asd2", Id = 2 },
                SubCategory = new SubCategory { Title = "asd2", Id = 2 },
                Content = "Content2",
                Title = "Title2",
                PictureId = 2,
                SeenCount = 2,
                Id = 2,
                Author = new ApplicationUser { FirstName = "asd2", LastName = "asd2", Id = "ads2", CreatedOn = DateTime.Now, ProfilePicture = new Image { ImageUrl = "aaaa2" } },
                Picture = new Image { ImageUrl = "testurl2" },
                CreatedOn = DateTime.Now,
                Comments = new List<Comment>(),
                Region = Region.Burgas,
            };
        }

        public static Article CreateThirdArticle()
        {
            return new Article
            {
                Category = new Category { Title = "asd3", Id = 3 },
                SubCategory = new SubCategory { Title = "asd3", Id = 3 },
                Content = "Content3",
                Title = "Title3",
                PictureId = 3,
                SeenCount = 3,
                Id = 3,
                Author = new ApplicationUser { FirstName = "asd3", LastName = "asd3", Id = "ads3", CreatedOn = DateTime.Now, ProfilePicture = new Image { ImageUrl = "aaaa3" } },
                Picture = new Image { ImageUrl = "testurl3" },
                CreatedOn = DateTime.Now,
                Comments = new List<Comment>(),
                Region = null,
            };
        }
    }
}
