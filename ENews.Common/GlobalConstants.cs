﻿namespace ENews.Common
{
    public static class GlobalConstants
    {
        public const int ArticlePerPage = 4;
        public const int AdministrationItemsPerPage = 10;

        public const string SystemEmail = "ENews@gmail.com";
        public const string SystemName = "ENews";
        public const string SystemUrl = "https://eunews.azurewebsites.net/";
        public const string SystemArticlePreviewUrl = "https://eunews.azurewebsites.net/Article/";
        public const string SystemImageUrl = "https://res.cloudinary.com/dijwyj1gn/image/upload/v1587629388/dw1pfnqqdb78mv4mdigf.jpg";
        //public const string ProfilePictureFill = "https://www.searchpng.com/wp-content/uploads/2019/02/Deafult-Profile-Pitcher.png";
        public const string ProfilePictureFill = "https://simpleicon.com/wp-content/uploads/user1.png";

        // Sorting
        // --Email
        public const string EmailSortBtn = "emailSort";
        public const string EmailSortTypeAsc = "emailAsc";
        public const string EmailSortTypeDesc = "emailDesc";

        // --CreatedOn
        public const string CreatedOnSortBtn = "createdOnSort";
        public const string CreatedOnTypeAsc = "createdOnAsc";
        public const string CreatedTypeOnDesc = "createdOnDesc";

        // --UserName
        public const string UsernameSortBtn = "usernameSort";
        public const string UsernameTypeAsc = "usernameAsc";
        public const string UsernamepeOnDesc = "usernameDesc";

        // Routes
        public const string CategoryRoute = "category";
        public const string UsernameRoute = "byUsername";
        public const string SubCategoryRoute = "subCategory";
        public const string LocalRoute = "local";
        public const string LocalByRegionRoute = "localByregion";

        public const string Country = "Bulgaria";

        // SharingUrls
        public const string FacebookShare = "https://www.facebook.com/sharer/sharer.php?u=";

        // Roles names
        public const string AdministratorRoleName = "Administrator";

        public const string ReporterRoleName = "Reporter";

        // Users seeding details
        public const string PhoneNumber = "0123456789";

        public const string BiographyFiller = "Donec turpis erat, scelerisque id euismod sit amet, fermentum vel dolor. Nulla facilisi. " +
            "Sed pellen tesque lectus et accu msan aliquam. Fusce lobortis cursus quam, id mattis sapien.";

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
