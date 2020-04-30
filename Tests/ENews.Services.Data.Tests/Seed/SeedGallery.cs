using ENews.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Services.Data.Tests.Seed
{
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
