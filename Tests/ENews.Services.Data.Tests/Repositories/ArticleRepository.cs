namespace ENews.Services.Data.Tests.Repositories
{
    using System;

    using ENews.Data;
    using ENews.Data.Models;
    using ENews.Data.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class ArticleRepository
    {
        public static EfDeletableEntityRepository<Article> Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var repository = new EfDeletableEntityRepository<Article>(new ApplicationDbContext(options));

            return repository;
        }
    }
}
