using Microsoft.EntityFrameworkCore.Migrations;

namespace SifflForums.Data.Migrations
{
    public partial class Table_BlacklistedPasswords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlacklistedPasswords",
                columns: table => new
                {
                    BlacklistedPasswordId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlacklistedPasswords", x => x.BlacklistedPasswordId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlacklistedPasswords");
        }
    }
}
