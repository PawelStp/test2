using Microsoft.EntityFrameworkCore.Migrations;

namespace Games.Infrastructure.Migrations
{
    public partial class RelationGameCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Games",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Games_CategoryId",
                table: "Games",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Categories_CategoryId",
                table: "Games",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Categories_CategoryId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_CategoryId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Games");
        }
    }
}
