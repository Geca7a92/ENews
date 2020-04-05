using System.Collections.Generic;

namespace ENews.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "ENews";

        public const string AdministratorRoleName = "Administrator";

        public const string AuthorRoleName = "Author";

        public const string LogedingUserRoleName = "User";

        public static readonly string[] BussinesSubcategories = { "Finance", "Energy", "Industry", "Tourism" };

        public static readonly string[] PoliticsSubcategories = { "Diplomacy", "Defence", "EU", "Domestic" };

        public static readonly string[] WorldSubcategories = { "Europe", "Asia", "America", "Africa" };

        public static readonly string[] SocietySubcategories = { "Education", "Culture", "Environment", "Crime" };

        public static readonly string[] SportSubcategories = { "Football", "MotoSport", "Fighting", "Chess" };

        public static readonly string[] CultureSubcategories = { "Film", "Art", "Books", "Tv" };
    }
}
