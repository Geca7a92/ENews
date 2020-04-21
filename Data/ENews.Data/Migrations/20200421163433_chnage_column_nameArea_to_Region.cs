using Microsoft.EntityFrameworkCore.Migrations;

namespace ENews.Data.Migrations
{
    public partial class chnage_column_nameArea_to_Region : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "Region",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "Area",
                table: "Articles",
                type: "int",
                nullable: true);
        }
    }
}
