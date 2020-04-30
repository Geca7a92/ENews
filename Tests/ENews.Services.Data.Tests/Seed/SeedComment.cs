namespace ENews.Services.Data.Tests.Seed
{
    using System;

    using ENews.Data.Models;

    public class SeedComment
    {
        public static Comment Create()
        {
            return new Comment
            {
                Content = "One",
                Id = 1,
                ArticleId = 1,
                UserId = "One",
                CreatedOn = DateTime.Now,
            };
        }

        public static Comment CreateSecond()
        {
            return new Comment
            {
                Content = "Two",
                Id = 2,
                ArticleId = 2,
                UserId = "Two",
                CreatedOn = DateTime.Now,
                IsDeleted=true
            };
        }

        public static Comment CreateThird()
        {
            return new Comment
            {
                Content = "Three",
                Id = 3,
                ArticleId = 3,
                UserId = "Three",
                CreatedOn = DateTime.Now,
            };
        }
    }
}
