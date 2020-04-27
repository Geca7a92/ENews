﻿using System.Collections.Generic;

namespace ENews.Common
{
    public static class GlobalConstants
    {
        public const int ArticlePerPage = 5;

        public const string SystemName = "ENews";
        public const string SystemUrl = "https://localhost:44319/";
        public const string SystemArticlePreviewUrl = "https://localhost:44319/Articles/Index/";
        public const string SystemImageUrl = "https://res.cloudinary.com/dijwyj1gn/image/upload/v1587629388/dw1pfnqqdb78mv4mdigf.jpg";
        //public const string ProfilePictureFill = "https://www.searchpng.com/wp-content/uploads/2019/02/Deafult-Profile-Pitcher.png";
        public const string ProfilePictureFill = "https://simpleicon.com/wp-content/uploads/user1.png";


        public const string Country = "Bulgaria";

        // Roles names
        public const string AdministratorRoleName = "Administrator";

        public const string ReporterRoleName = "Reporter";

        // Users seeding details
        public const string PhoneNumber = "0123456789";

        // Admin account info
        public const string AdminPassword = "adminadmin";

        public const string AdminUsername = "admin";

        public const string AdminFirstName = "Admin";

        public const string AdminLastName = "Administratov";

        public const string AdminEmailName = "admin@abv.bg";

        // Reporter account info
        public const string ReporterPassword = "reporter";

        public const string ReporterUsername = "reporter";

        public const string ReporterFirstName = "Reporer";

        public const string ReporterLastName = "Reporerov";

        public const string ReporterEmailName = "reporter@abv.bg";

        public static readonly string[] BussinesSubcategories = { "Finance", "Energy", "Industry", "Tourism" };

        public static readonly string[] PoliticsSubcategories = { "Diplomacy", "Defence", "EU", "Domestic" };

        public static readonly string[] WorldSubcategories = { "Europe", "Asia", "America", "Africa" };

        public static readonly string[] SocietySubcategories = { "Education", "Culture", "Environment", "Crime" };

        public static readonly string[] SportSubcategories = { "Football", "MotoSport", "Fighting", "Chess" };

        public static readonly string[] CultureSubcategories = { "Film", "Art", "Books", "Tv" };
    }
}
