using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class SwitchedWrongRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_SensorGroups_SensorGroupId",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_SensorGroupId",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SensorGroupId",
                table: "Sensors");

            migrationBuilder.AddColumn<Guid>(
                name: "SensorId",
                table: "SensorGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SensorGroups_SensorId",
                table: "SensorGroups",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorGroups_Sensors_SensorId",
                table: "SensorGroups",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorGroups_Sensors_SensorId",
                table: "SensorGroups");

            migrationBuilder.DropIndex(
                name: "IX_SensorGroups_SensorId",
                table: "SensorGroups");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "SensorGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "SensorGroupId",
                table: "Sensors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SensorGroupId",
                table: "Sensors",
                column: "SensorGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_SensorGroups_SensorGroupId",
                table: "Sensors",
                column: "SensorGroupId",
                principalTable: "SensorGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
