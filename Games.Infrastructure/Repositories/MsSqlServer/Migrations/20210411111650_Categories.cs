using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Games.Infrastructure.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Strategiczna" },
                    { 2L, "Sportowa" },
                    { 3L, "Wyścigówki" },
                    { 4L, "Wojenna" },
                    { 5L, "FPS" },
                    { 6L, "MMORPG" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "ReleaseDate", "Title", "Url" },
                values: new object[] { 1L, "PRzygowa fjan gra", new DateTimeOffset(new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Wiedźmin Dziki Gon", "https://i.ytimg.com/vi/rhsf_Fqa9JE/maxresdefault.jpg" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "ReleaseDate", "Title", "Url" },
                values: new object[] { 2L, "Skoki", new DateTimeOffset(new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Ski Deluxe Jumping", "https://www.instalki.pl/images/newsy/11-2020/dsj-2-android-01.jpg" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "ReleaseDate", "Title", "Url" },
                values: new object[] { 3L, "Strzelanka", new DateTimeOffset(new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Cyberpunk 2077", "https://img.android.com.pl/images/user-images/2021/03/aktualizacja-1.2-Cyberpunk-2077_1.jpg" });
        }
    }
}
