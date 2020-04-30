namespace ENews.Services.Data.Tests
{
    using System;

    using ENews.Data;
    using Microsoft.EntityFrameworkCore;

    public class ContextTest
    {
        public static ApplicationDbContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var context = new ApplicationDbContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }
    }
}
