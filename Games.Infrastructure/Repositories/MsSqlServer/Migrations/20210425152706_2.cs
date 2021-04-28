using Microsoft.EntityFrameworkCore.Migrations;

namespace Games.Infrastructure.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    GameId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rate_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rate_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rate_GameId",
                table: "Rate",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_UserId",
                table: "Rate",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rate");
        }
    }
}
