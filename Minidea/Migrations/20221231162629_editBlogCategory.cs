using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minidea.Migrations
{
    public partial class editBlogCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogsCategories_BlogsCategories_BlogsCategoriesId",
                table: "BlogsCategories");

            migrationBuilder.DropIndex(
                name: "IX_BlogsCategories_BlogsCategoriesId",
                table: "BlogsCategories");

            migrationBuilder.DropColumn(
                name: "BlogsCategoriesId",
                table: "BlogsCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogsCategoriesId",
                table: "BlogsCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogsCategories_BlogsCategoriesId",
                table: "BlogsCategories",
                column: "BlogsCategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogsCategories_BlogsCategories_BlogsCategoriesId",
                table: "BlogsCategories",
                column: "BlogsCategoriesId",
                principalTable: "BlogsCategories",
                principalColumn: "Id");
        }
    }
}
