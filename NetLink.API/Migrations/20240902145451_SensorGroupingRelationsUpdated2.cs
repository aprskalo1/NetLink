using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class SensorGroupingRelationsUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_SensorGroups_SensorGroupId",
                table: "Sensors");

            migrationBuilder.AlterColumn<Guid>(
                name: "SensorGroupId",
                table: "Sensors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_SensorGroups_SensorGroupId",
                table: "Sensors",
                column: "SensorGroupId",
                principalTable: "SensorGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_SensorGroups_SensorGroupId",
                table: "Sensors");

            migrationBuilder.AlterColumn<Guid>(
                name: "SensorGroupId",
                table: "Sensors",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_SensorGroups_SensorGroupId",
                table: "Sensors",
                column: "SensorGroupId",
                principalTable: "SensorGroups",
                principalColumn: "Id");
        }
    }
}
