using ENews.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            //builder
            //    .HasOne(a => a.Gallery)
            //    .WithOne(g => g.Article)
            //    .HasForeignKey<Gallery>(g => g.ArticleId);
        }
    }
}
