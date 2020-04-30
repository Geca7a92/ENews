﻿using ENews.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENews.Services.Data.Tests.Seed
{
    public class SeedCategory
    {
        public static Category Create()
        {
            return new Category
            {
                Title = "One",
                Id = 1,
                Description = "One",
            };
        }

        public static Category CreateSecond()
        {
            return new Category
            {
                Title = "Two",
                Id = 2,
                Description = "Two",
                IsDeleted=true,
            };
        }

        public static Category CreateThird()
        {
            return new Category
            {
                Title = "three",
                Id = 3,
                Description = "three",
                SubCategories= new List<SubCategory> { new SubCategory { Title = "threethree" } },
            };
        }
    }
}
