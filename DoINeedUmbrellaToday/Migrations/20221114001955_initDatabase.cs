using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoINeedUmbrellaToday.Migrations
{
    /// <inheritdoc />
    public partial class initDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "TelegramChats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: true).Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "ru"),
                    State = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "lang")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChats", x => x.Id);

                });

            migrationBuilder.CreateTable(
                name: "TelegramBotProgresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: true),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: true),
                    LastQuery = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotProgresses", x => x.Id);
                    table.ForeignKey("TelegramChatIdForeign", x => x.Id, "TelegramChats", "Id");
                });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelegramBotProgresses");

            migrationBuilder.DropTable(
                name: "TelegramChats");
        }
    }
}
