using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class SensorGroupingRelationsUpdated3 : Migration
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

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "SensorGroups");

            migrationBuilder.CreateTable(
                name: "SensorGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SensorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorGroup");

            migrationBuilder.AddColumn<Guid>(
                name: "SensorGroupId",
                table: "Sensors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SensorId",
                table: "SensorGroups",
                type: "uniqueidentifier",
                nullable: true);

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
