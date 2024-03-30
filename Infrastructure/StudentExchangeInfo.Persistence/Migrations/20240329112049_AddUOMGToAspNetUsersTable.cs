using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchangeInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUOMGToAspNetUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "UOMG",
                table: "AspNetUsers",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UOMG",
                table: "AspNetUsers");
        }
    }
}
