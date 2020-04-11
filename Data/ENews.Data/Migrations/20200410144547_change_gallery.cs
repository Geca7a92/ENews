using Microsoft.EntityFrameworkCore.Migrations;

namespace ENews.Data.Migrations
{
    public partial class change_gallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Articles_ArticleId",
                table: "Galleries");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_ArticleId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Galleries");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_GalleryId",
                table: "Articles",
                column: "GalleryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Galleries_GalleryId",
                table: "Articles",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Galleries_GalleryId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_GalleryId",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Galleries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_ArticleId",
                table: "Galleries",
                column: "ArticleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Articles_ArticleId",
                table: "Galleries",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
