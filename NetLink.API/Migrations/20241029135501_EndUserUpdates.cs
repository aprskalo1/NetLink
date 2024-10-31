using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetLink.API.Migrations
{
    /// <inheritdoc />
    public partial class EndUserUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "EndUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "EndUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "EndUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "EndUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "EndUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "EndUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "EndUsers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "EndUsers");
        }
    }
}
