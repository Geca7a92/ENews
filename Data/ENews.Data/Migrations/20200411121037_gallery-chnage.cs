using Microsoft.EntityFrameworkCore.Migrations;

namespace ENews.Data.Migrations
{
    public partial class gallerychnage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_GalleryId",
                table: "Articles");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_GalleryId",
                table: "Articles",
                column: "GalleryId",
                unique: true,
                filter: "[GalleryId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_GalleryId",
                table: "Articles");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_GalleryId",
                table: "Articles",
                column: "GalleryId");
        }
    }
}
