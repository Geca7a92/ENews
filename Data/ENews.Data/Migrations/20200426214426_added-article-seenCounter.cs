using Microsoft.EntityFrameworkCore.Migrations;

namespace ENews.Data.Migrations
{
    public partial class addedarticleseenCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressText",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "SeenCount",
                table: "Articles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenCount",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "AddressText",
                table: "Addresses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }
    }
}
