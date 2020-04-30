using ENews.Data;
using ENews.Data.Models;
using ENews.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Services.Data.Tests.Repositories
{
    public class CategoryRepository
    {
        public static EfDeletableEntityRepository<Category> Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options));

            return repository;
        }
    }
}
