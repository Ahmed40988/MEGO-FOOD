using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Web.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refactoryRestaurnatEntity10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "UserAddresses",
                type: "geography",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasFastDelivery",
                table: "Restaurants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasFreeDelivery",
                table: "Restaurants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasOffers",
                table: "Restaurants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Restaurants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Restaurants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "RestaurantAdresses",
                type: "geography",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "HasFastDelivery",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "HasFreeDelivery",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "HasOffers",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "RestaurantAdresses");
        }
    }
}
