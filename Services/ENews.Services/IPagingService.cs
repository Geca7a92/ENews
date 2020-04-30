namespace ENews.Services
{
    using ENews.Data.Models.Enums;

    public interface IPagingService
    {
        int CountSkip(int page, int count);

        int GetPagesCountByRegion(Region? region);

        int PagesCount(int itemCount, int itemsPerPage);

        int SetPage(int page, int pagesCount);
    }
}
