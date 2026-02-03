using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BaseCatgoryId",
                table: "RestaurantCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdon",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByid",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedon",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Createdon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedByid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updatedon = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseCategories_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantCategories_BaseCatgoryId",
                table: "RestaurantCategories",
                column: "BaseCatgoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseCategories_AppUserId",
                table: "BaseCategories",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantCategories_BaseCategories_BaseCatgoryId",
                table: "RestaurantCategories",
                column: "BaseCatgoryId",
                principalTable: "BaseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantCategories_BaseCategories_BaseCatgoryId",
                table: "RestaurantCategories");

            migrationBuilder.DropTable(
                name: "BaseCategories");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantCategories_BaseCatgoryId",
                table: "RestaurantCategories");

            migrationBuilder.DropColumn(
                name: "BaseCatgoryId",
                table: "RestaurantCategories");

            migrationBuilder.DropColumn(
                name: "Createdon",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedByid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Updatedon",
                table: "AspNetUsers");
        }
    }
}
