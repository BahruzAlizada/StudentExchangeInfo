using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchangeInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateMotivationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Motivations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotivationAZE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotivationENG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motivations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motivations");
        }
    }
}
