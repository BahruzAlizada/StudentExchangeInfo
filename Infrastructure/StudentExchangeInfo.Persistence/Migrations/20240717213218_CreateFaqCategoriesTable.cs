using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchangeInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateFaqCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Faqs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FaqCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_CategoryId",
                table: "Faqs",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_FaqCategories_CategoryId",
                table: "Faqs",
                column: "CategoryId",
                principalTable: "FaqCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_FaqCategories_CategoryId",
                table: "Faqs");

            migrationBuilder.DropTable(
                name: "FaqCategories");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_CategoryId",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Faqs");
        }
    }
}
