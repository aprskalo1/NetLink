using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class SensorGroupingRelationsUpdated4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EndUserSensorGroups_SensorGroups_SensorGroupId",
                table: "EndUserSensorGroups");

            migrationBuilder.DropTable(
                name: "SensorGroup");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "SensorGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "SensorGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SensorId",
                table: "SensorGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorGroups_GroupId",
                table: "SensorGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorGroups_SensorId",
                table: "SensorGroups",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserSensorGroups_Groups_SensorGroupId",
                table: "EndUserSensorGroups",
                column: "SensorGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SensorGroups_Groups_GroupId",
                table: "SensorGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_EndUserSensorGroups_Groups_SensorGroupId",
                table: "EndUserSensorGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SensorGroups_Groups_GroupId",
                table: "SensorGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SensorGroups_Sensors_SensorId",
                table: "SensorGroups");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_SensorGroups_GroupId",
                table: "SensorGroups");

            migrationBuilder.DropIndex(
                name: "IX_SensorGroups_SensorId",
                table: "SensorGroups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "SensorGroups");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "SensorGroups");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "SensorGroups",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SensorGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SensorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorGroup_SensorGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "SensorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SensorGroup_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorGroup_GroupId",
                table: "SensorGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorGroup_SensorId",
                table: "SensorGroup",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EndUserSensorGroups_SensorGroups_SensorGroupId",
                table: "EndUserSensorGroups",
                column: "SensorGroupId",
                principalTable: "SensorGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
