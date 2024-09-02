using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class MakeSensorNullableInGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorGroups_Sensors_SensorId",
                table: "SensorGroups");

            migrationBuilder.AlterColumn<Guid>(
                name: "SensorId",
                table: "SensorGroups",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorGroups_Sensors_SensorId",
                table: "SensorGroups",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorGroups_Sensors_SensorId",
                table: "SensorGroups");

            migrationBuilder.AlterColumn<Guid>(
                name: "SensorId",
                table: "SensorGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SensorGroups_Sensors_SensorId",
                table: "SensorGroups",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
