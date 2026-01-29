using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_RestaurantCategories_RestaurantCategoryId",
                table: "MenuCategories");

            migrationBuilder.RenameColumn(
                name: "RestaurantCategoryId",
                table: "MenuCategories",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuCategories_RestaurantCategoryId",
                table: "MenuCategories",
                newName: "IX_MenuCategories_RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_RestaurantCategories_RestaurantId",
                table: "MenuCategories",
                column: "RestaurantId",
                principalTable: "RestaurantCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_RestaurantCategories_RestaurantId",
                table: "MenuCategories");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "MenuCategories",
                newName: "RestaurantCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuCategories_RestaurantId",
                table: "MenuCategories",
                newName: "IX_MenuCategories_RestaurantCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_RestaurantCategories_RestaurantCategoryId",
                table: "MenuCategories",
                column: "RestaurantCategoryId",
                principalTable: "RestaurantCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
