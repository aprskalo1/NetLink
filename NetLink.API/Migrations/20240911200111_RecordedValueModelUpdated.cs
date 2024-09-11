using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class RecordedValueModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EndUserSensors_SensorId",
                table: "EndUserSensors");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RecordedValues",
                type: "float",
                maxLength: 50,
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EndUserSensors_SensorId",
                table: "EndUserSensors",
                column: "SensorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EndUserSensors_SensorId",
                table: "EndUserSensors");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "RecordedValues",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_EndUserSensors_SensorId",
                table: "EndUserSensors",
                column: "SensorId");
        }
    }
}
