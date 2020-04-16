using Microsoft.EntityFrameworkCore.Migrations;

namespace ENews.Data.Migrations
{
    public partial class fixCategoryPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desctription",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Desctription",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
