using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class SensorGroupingRelationsUpdated5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndUserSensorGroups");

            migrationBuilder.CreateTable(
                name: "EndUserGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndUserGroups_EndUsers_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "EndUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EndUserGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndUserGroups_EndUserId",
                table: "EndUserGroups",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserGroups_GroupId",
                table: "EndUserGroups",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndUserGroups");

            migrationBuilder.CreateTable(
                name: "EndUserSensorGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    SensorGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        name: "FK_EndUserSensorGroups_Groups_SensorGroupId",
                        column: x => x.SensorGroupId,
                        principalTable: "Groups",
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
        }
    }
}
