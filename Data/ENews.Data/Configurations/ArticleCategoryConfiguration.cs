using ENews.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Data.Configurations
{
    public class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder
                .HasKey(ac => new
                {
                    ac.ArticleId,
                    ac.CategoryId,
                });

            builder
                .HasOne(ac => ac.Article)
                .WithMany(a => a.Categories)
                .HasForeignKey(ac => ac.ArticleId);

            builder
                .HasOne(ac => ac.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(ac => ac.CategoryId);
        }
    }
}
