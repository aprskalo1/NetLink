using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEndUserSensorAndEndUserSensorGroupEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndUserSensorGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SensorGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserSensorGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndUserSensorGroups_EndUsers_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "EndUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EndUserSensorGroups_SensorGroups_SensorGroupId",
                        column: x => x.SensorGroupId,
                        principalTable: "SensorGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndUserSensors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SensorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserSensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndUserSensors_EndUsers_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "EndUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EndUserSensors_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndUserSensorGroups_EndUserId",
                table: "EndUserSensorGroups",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserSensorGroups_SensorGroupId",
                table: "EndUserSensorGroups",
                column: "SensorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserSensors_EndUserId",
                table: "EndUserSensors",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserSensors_SensorId",
                table: "EndUserSensors",
                column: "SensorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndUserSensorGroups");

            migrationBuilder.DropTable(
                name: "EndUserSensors");
        }
    }
}
