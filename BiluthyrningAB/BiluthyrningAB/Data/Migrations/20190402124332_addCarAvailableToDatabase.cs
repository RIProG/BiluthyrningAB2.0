using Microsoft.EntityFrameworkCore.Migrations;

namespace BiluthyrningAB.Data.Migrations
{
    public partial class addCarAvailableToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DistanceInKm",
                table: "Car",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Car",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Car");

            migrationBuilder.AlterColumn<double>(
                name: "DistanceInKm",
                table: "Car",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
