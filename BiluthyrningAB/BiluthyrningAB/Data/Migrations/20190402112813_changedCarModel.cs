using Microsoft.EntityFrameworkCore.Migrations;

namespace BiluthyrningAB.Data.Migrations
{
    public partial class changedCarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DistanceInKm",
                table: "Car",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DistanceInKm",
                table: "Car",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
