using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchangeInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateExchangeProgramAndExchangeProgramDocumentAndExchangeProgramConditionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeProgram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    ExchangeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeProgram_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeProgramCondition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeProgramId = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeProgramCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeProgramCondition_ExchangeProgram_ExchangeProgramId",
                        column: x => x.ExchangeProgramId,
                        principalTable: "ExchangeProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeProgramDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeProgramId = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeProgramDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeProgramDocument_ExchangeProgram_ExchangeProgramId",
                        column: x => x.ExchangeProgramId,
                        principalTable: "ExchangeProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeProgram_AppUserId",
                table: "ExchangeProgram",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeProgramCondition_ExchangeProgramId",
                table: "ExchangeProgramCondition",
                column: "ExchangeProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeProgramDocument_ExchangeProgramId",
                table: "ExchangeProgramDocument",
                column: "ExchangeProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeProgramCondition");

            migrationBuilder.DropTable(
                name: "ExchangeProgramDocument");

            migrationBuilder.DropTable(
                name: "ExchangeProgram");
        }
    }
}
