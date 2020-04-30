namespace ENews.Services.Data.Tests.Seed
{
    using ENews.Data.Models;

    public class SeedGallery
    {
        public static Gallery Create()
        {
            return new Gallery
            {
                Id=1,
            };
        }

        public static Gallery CreateSecond()
        {
            return new Gallery
            {
                Id = 1,
            };
        }

        public static Gallery CreateThird()
        {
            return new Gallery
            {
                Id = 1,
            };
        }
    }
}
