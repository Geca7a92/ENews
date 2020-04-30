namespace ENews.Services
{
    using System;

    using ENews.Common;
    using ENews.Data.Models.Enums;
    using ENews.Services.Data;

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

            return this.PagesCount(count, GlobalConstants.ArticlePerPage);
        }

        public int CountSkip(int page, int count)
        {
            if (page < 1)
            {
                page = 1;
            }

            return (page - 1) * count;
        }

        public int PagesCount(int itemCount, int itemsPerPage)
        {
            if (itemCount == 0)
            {
                itemCount = 1;
            }

            var pagesCount = (int)Math.Ceiling((double)itemCount / itemsPerPage);
            return pagesCount;
        }

        public int SetPage(int page, int pagesCount)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (page > pagesCount)
            {
                return pagesCount;
            }

            return page;
        }
    }
}
