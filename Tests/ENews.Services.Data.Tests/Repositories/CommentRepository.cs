namespace ENews.Services.Data.Tests.Repositories
{
    using System;

    using ENews.Data;
    using ENews.Data.Models;
    using ENews.Data.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class CommentRepository
    {
        public static EfDeletableEntityRepository<Comment> Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var repository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options));

            return repository;
        }
    }
}
