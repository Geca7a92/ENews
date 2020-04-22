using ENews.Data.Models.Enums;
using System.Collections.Generic;
using System.Text;

namespace ENews.Services
{
    public interface IPagingService
    {
        int CountSkip(int page);

        int GetPagesCountByRegion(Region? region);

        int PagesCount(int articlesCount);
    }
}
