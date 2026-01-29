using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updaterestaurants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_RestaurantCategories_RestaurantId",
                table: "MenuCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantCategories_AspNetUsers_AppUserId",
                table: "RestaurantCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantCategories_AspNetUsers_userid",
                table: "RestaurantCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantCategories_BaseCategories_BaseCatgoryId",
                table: "RestaurantCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantCategories",
                table: "RestaurantCategories");

            migrationBuilder.RenameTable(
                name: "RestaurantCategories",
                newName: "Restaurants");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantCategories_userid",
                table: "Restaurants",
                newName: "IX_Restaurants_userid");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantCategories_BaseCatgoryId",
                table: "Restaurants",
                newName: "IX_Restaurants_BaseCatgoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantCategories_AppUserId",
                table: "Restaurants",
                newName: "IX_Restaurants_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_Restaurants_RestaurantId",
                table: "MenuCategories",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId",
                table: "Restaurants",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_userid",
                table: "Restaurants",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_BaseCategories_BaseCatgoryId",
                table: "Restaurants",
                column: "BaseCatgoryId",
                principalTable: "BaseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_Restaurants_RestaurantId",
                table: "MenuCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_userid",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_BaseCategories_BaseCatgoryId",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "RestaurantCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_userid",
                table: "RestaurantCategories",
                newName: "IX_RestaurantCategories_userid");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_BaseCatgoryId",
                table: "RestaurantCategories",
                newName: "IX_RestaurantCategories_BaseCatgoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_AppUserId",
                table: "RestaurantCategories",
                newName: "IX_RestaurantCategories_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantCategories",
                table: "RestaurantCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_RestaurantCategories_RestaurantId",
                table: "MenuCategories",
                column: "RestaurantId",
                principalTable: "RestaurantCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantCategories_AspNetUsers_AppUserId",
                table: "RestaurantCategories",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantCategories_AspNetUsers_userid",
                table: "RestaurantCategories",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantCategories_BaseCategories_BaseCatgoryId",
                table: "RestaurantCategories",
                column: "BaseCatgoryId",
                principalTable: "BaseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
