using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchangeInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsBacheolorToAspNetUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBacheolor",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBacheolor",
                table: "AspNetUsers");
        }
    }
}
