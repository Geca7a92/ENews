using ENews.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Data.Configurations
{
    public class ArticleSubCategoryConfiguration : IEntityTypeConfiguration<ArticleSubCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleSubCategory> builder)
        {
            builder
                .HasKey(asc => new
                {
                    asc.ArticleId,
                    asc.SubCategoryId,
                });

            builder
                .HasOne(asc => asc.Article)
                .WithMany(a => a.SubCategories)
                .HasForeignKey(asc => asc.ArticleId);

            builder
                .HasOne(asc => asc.SubCategory)
                .WithMany(sc => sc.Articles)
                .HasForeignKey(asc => asc.SubCategoryId);
        }
    }
}
