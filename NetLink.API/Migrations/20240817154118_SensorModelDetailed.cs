using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class SensorModelDetailed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceDescription",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceLocation",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceType",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasurementUnit",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceDescription",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "DeviceLocation",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "MeasurementUnit",
                table: "Sensors");
        }
    }
}
