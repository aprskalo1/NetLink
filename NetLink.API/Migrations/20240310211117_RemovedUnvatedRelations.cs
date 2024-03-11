using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnvatedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_EndUsers_EndUserId",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_EndUserId",
                table: "Sensors");

            migrationBuilder.AlterColumn<string>(
                name: "EndUserId",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EndUserId",
                table: "Sensors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_EndUserId",
                table: "Sensors",
                column: "EndUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_EndUsers_EndUserId",
                table: "Sensors",
                column: "EndUserId",
                principalTable: "EndUsers",
                principalColumn: "Id");
        }
    }
}
