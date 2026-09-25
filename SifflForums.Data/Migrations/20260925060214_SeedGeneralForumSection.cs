using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SifflForums.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGeneralForumSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ForumSections",
                columns: new[] { "Id", "CreatedAtUtc", "CreatedBy", "Description", "IsPrivate", "ModifiedAtUtc", "ModifiedBy", "Name" },
                values: new object[] { "8f1c6a3e-2b1d-4c1e-9f0a-5d3b7e2a1c40", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "General Discussions", false, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "General" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ForumSections",
                keyColumn: "Id",
                keyValue: "8f1c6a3e-2b1d-4c1e-9f0a-5d3b7e2a1c40");
        }
    }
}
