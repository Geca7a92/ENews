namespace ENews.Data
{
    using System.Linq;

    using ENews.Data.Common.Models;
    using ENews.Data.Models;
    using Microsoft.EntityFrameworkCore;

    internal static class EntityIndexesConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            // IDeletableEntity.IsDeleted index

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(ap => ap.Address)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey<Address>(a => a.ApplicationUserId);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Gallery)
                .WithOne(g => g.Article)
                .HasForeignKey<Gallery>(g => g.ArticleId);

            var deletableEntityTypes = modelBuilder.Model
                .GetEntityTypes()
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                modelBuilder.Entity(deletableEntityType.ClrType).HasIndex(nameof(IDeletableEntity.IsDeleted));
            }
        }
    }
}
