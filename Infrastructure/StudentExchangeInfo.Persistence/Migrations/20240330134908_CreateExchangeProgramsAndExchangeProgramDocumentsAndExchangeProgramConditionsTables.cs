using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchangeInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateExchangeProgramsAndExchangeProgramDocumentsAndExchangeProgramConditionsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeProgram_AspNetUsers_AppUserId",
                table: "ExchangeProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeProgramCondition_ExchangeProgram_ExchangeProgramId",
                table: "ExchangeProgramCondition");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeProgramDocument_ExchangeProgram_ExchangeProgramId",
                table: "ExchangeProgramDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeProgramDocument",
                table: "ExchangeProgramDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeProgramCondition",
                table: "ExchangeProgramCondition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeProgram",
                table: "ExchangeProgram");

            migrationBuilder.RenameTable(
                name: "ExchangeProgramDocument",
                newName: "ExchangeProgramDocuments");

            migrationBuilder.RenameTable(
                name: "ExchangeProgramCondition",
                newName: "ExchangeProgramConditions");

            migrationBuilder.RenameTable(
                name: "ExchangeProgram",
                newName: "ExchangePrograms");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeProgramDocument_ExchangeProgramId",
                table: "ExchangeProgramDocuments",
                newName: "IX_ExchangeProgramDocuments_ExchangeProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeProgramCondition_ExchangeProgramId",
                table: "ExchangeProgramConditions",
                newName: "IX_ExchangeProgramConditions_ExchangeProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeProgram_AppUserId",
                table: "ExchangePrograms",
                newName: "IX_ExchangePrograms_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeProgramDocuments",
                table: "ExchangeProgramDocuments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeProgramConditions",
                table: "ExchangeProgramConditions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangePrograms",
                table: "ExchangePrograms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeProgramConditions_ExchangePrograms_ExchangeProgramId",
                table: "ExchangeProgramConditions",
                column: "ExchangeProgramId",
                principalTable: "ExchangePrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeProgramDocuments_ExchangePrograms_ExchangeProgramId",
                table: "ExchangeProgramDocuments",
                column: "ExchangeProgramId",
                principalTable: "ExchangePrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangePrograms_AspNetUsers_AppUserId",
                table: "ExchangePrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeProgramConditions_ExchangePrograms_ExchangeProgramId",
                table: "ExchangeProgramConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeProgramDocuments_ExchangePrograms_ExchangeProgramId",
                table: "ExchangeProgramDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangePrograms_AspNetUsers_AppUserId",
                table: "ExchangePrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangePrograms",
                table: "ExchangePrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeProgramDocuments",
                table: "ExchangeProgramDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExchangeProgramConditions",
                table: "ExchangeProgramConditions");

            migrationBuilder.RenameTable(
                name: "ExchangePrograms",
                newName: "ExchangeProgram");

            migrationBuilder.RenameTable(
                name: "ExchangeProgramDocuments",
                newName: "ExchangeProgramDocument");

            migrationBuilder.RenameTable(
                name: "ExchangeProgramConditions",
                newName: "ExchangeProgramCondition");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangePrograms_AppUserId",
                table: "ExchangeProgram",
                newName: "IX_ExchangeProgram_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeProgramDocuments_ExchangeProgramId",
                table: "ExchangeProgramDocument",
                newName: "IX_ExchangeProgramDocument_ExchangeProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeProgramConditions_ExchangeProgramId",
                table: "ExchangeProgramCondition",
                newName: "IX_ExchangeProgramCondition_ExchangeProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeProgram",
                table: "ExchangeProgram",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeProgramDocument",
                table: "ExchangeProgramDocument",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExchangeProgramCondition",
                table: "ExchangeProgramCondition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeProgram_AspNetUsers_AppUserId",
                table: "ExchangeProgram",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeProgramCondition_ExchangeProgram_ExchangeProgramId",
                table: "ExchangeProgramCondition",
                column: "ExchangeProgramId",
                principalTable: "ExchangeProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeProgramDocument_ExchangeProgram_ExchangeProgramId",
                table: "ExchangeProgramDocument",
                column: "ExchangeProgramId",
                principalTable: "ExchangeProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
