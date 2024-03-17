using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class MajorDatabaseChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUsers_EndUsers_EndUserId",
                table: "DeveloperUsers");

            migrationBuilder.DropColumn(
                name: "RecordedValue",
                table: "Sensors");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Sensors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SensorGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EndUserSensors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EndUserSensorGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "EndUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EndUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "EndUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndUserId",
                table: "DeveloperUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DeveloperUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Developers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Developers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Developers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperUsers_EndUsers_EndUserId",
                table: "DeveloperUsers",
                column: "EndUserId",
                principalTable: "EndUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUsers_EndUsers_EndUserId",
                table: "DeveloperUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SensorGroups");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EndUserSensors");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EndUserSensorGroups");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "EndUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EndUsers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EndUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DeveloperUsers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Developers");

            migrationBuilder.AddColumn<string>(
                name: "RecordedValue",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndUserId",
                table: "DeveloperUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperUsers_EndUsers_EndUserId",
                table: "DeveloperUsers",
                column: "EndUserId",
                principalTable: "EndUsers",
                principalColumn: "Id");
        }
    }
}
