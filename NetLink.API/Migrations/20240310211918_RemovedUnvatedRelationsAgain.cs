using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnvatedRelationsAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorGroups_EndUsers_EndUserId",
                table: "SensorGroups");

            migrationBuilder.DropIndex(
                name: "IX_SensorGroups_EndUserId",
                table: "SensorGroups");

            migrationBuilder.DropColumn(
                name: "EndUserId",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "EndUserId",
                table: "SensorGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndUserId",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndUserId",
                table: "SensorGroups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SensorGroups_EndUserId",
                table: "SensorGroups",
                column: "EndUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorGroups_EndUsers_EndUserId",
                table: "SensorGroups",
                column: "EndUserId",
                principalTable: "EndUsers",
                principalColumn: "Id");
        }
    }
}
