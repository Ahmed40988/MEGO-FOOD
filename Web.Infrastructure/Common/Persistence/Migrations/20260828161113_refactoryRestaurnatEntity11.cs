using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Web.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refactoryRestaurnatEntity11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "UserAddresses",
                type: "geography",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geography",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "RestaurantAdresses",
                type: "geography",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geography",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "UserAddresses",
                type: "geography",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geography");

            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "RestaurantAdresses",
                type: "geography",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geography");
        }
    }
}
