using ENews.Common;
using ENews.Data.Models.Enums;
using ENews.Services.Data;
using System;

namespace ENews.Services
{
    public class PagingService : IPagingService
    {
        private readonly IArticlesService articlesService;

        public PagingService(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public int GetPagesCountByRegion(Region? region)
        {
            var count = 0;

            if (region.HasValue)
            {
                count = this.articlesService.GetCountByRegion(region.Value);
            }
            else
            {
                count = this.articlesService.GetCountOfLocalArticles();
            }

            return this.PagesCount(count);
        }

        public int CountSkip(int page)
        {
            return (page - 1) * GlobalConstants.ArticlePerPage;
        }

        public int PagesCount(int articlesCount)
        {
            if (articlesCount == 0)
            {
                articlesCount = 1;
            }

            var pagesCount = (int)Math.Ceiling((double)articlesCount / GlobalConstants.ArticlePerPage);
            return pagesCount;
        }
    }
}
