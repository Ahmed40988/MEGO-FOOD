using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class alterRestaurants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_userid",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_userid",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AppUserId1",
                table: "Restaurants",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId",
                table: "Restaurants",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId1",
                table: "Restaurants",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_AppUserId1",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_AppUserId1",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_userid",
                table: "Restaurants",
                column: "userid");

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
        }
    }
}
