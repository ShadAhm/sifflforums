using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SifflForums.Data.Migrations
{
    public partial class Initial : Migration
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    LastPasswordResetUtc = table.Column<DateTime>(nullable: false),
                    RegisteredAtUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "VotingBoxes",
                columns: table => new
                {
                    VotingBoxId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingBoxes", x => x.VotingBoxId);
                });

            migrationBuilder.CreateTable(
                name: "ForumSections",
                columns: table => new
                {
                    ForumSectionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsPrivate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumSections", x => x.ForumSectionId);
                    table.ForeignKey(
                        name: "FK_ForumSections_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumSections_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Upvotes",
                columns: table => new
                {
                    UpvoteId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VotingBoxId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upvotes", x => x.UpvoteId);
                    table.UniqueConstraint("AK_Upvotes_VotingBoxId_UserId", x => new { x.VotingBoxId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Upvotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Upvotes_VotingBoxes_VotingBoxId",
                        column: x => x.VotingBoxId,
                        principalTable: "VotingBoxes",
                        principalColumn: "VotingBoxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SubmissionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    VotingBoxId = table.Column<int>(nullable: false),
                    ForumSectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_Submissions_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_ForumSections_ForumSectionId",
                        column: x => x.ForumSectionId,
                        principalTable: "ForumSections",
                        principalColumn: "ForumSectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_VotingBoxes_VotingBoxId",
                        column: x => x.VotingBoxId,
                        principalTable: "VotingBoxes",
                        principalColumn: "VotingBoxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    SubmissionId = table.Column<int>(nullable: false),
                    VotingBoxId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "SubmissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_VotingBoxes_VotingBoxId",
                        column: x => x.VotingBoxId,
                        principalTable: "VotingBoxes",
                        principalColumn: "VotingBoxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "LastPasswordResetUtc", "Password", "RegisteredAtUtc", "Salt", "Username" },
                values: new object[] { 1, "system@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "suchAPrettyHouse", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "suchAPrettyGarden", "System" });

            migrationBuilder.InsertData(
                table: "VotingBoxes",
                column: "VotingBoxId",
                value: 1);

            migrationBuilder.InsertData(
                table: "ForumSections",
                columns: new[] { "ForumSectionId", "CreatedAtUtc", "CreatedBy", "Description", "IsPrivate", "ModifiedAtUtc", "ModifiedBy", "Name" },
                values: new object[] { 1, new DateTime(2019, 8, 30, 6, 53, 57, 703, DateTimeKind.Utc).AddTicks(6023), 1, "General Discussions", false, new DateTime(2019, 8, 30, 6, 53, 57, 703, DateTimeKind.Utc).AddTicks(6988), 1, "General" });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "CreatedAtUtc", "CreatedBy", "ForumSectionId", "ModifiedAtUtc", "ModifiedBy", "Text", "Title", "UserId", "VotingBoxId" },
                values: new object[] { 1, new DateTime(2019, 8, 30, 6, 53, 57, 704, DateTimeKind.Utc).AddTicks(2425), 1, 1, new DateTime(2019, 8, 30, 6, 53, 57, 704, DateTimeKind.Utc).AddTicks(2434), 1, "Simple Forums for Learning", "Welcome to Siffl Forums", 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedBy",
                table: "Comments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ModifiedBy",
                table: "Comments",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SubmissionId",
                table: "Comments",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VotingBoxId",
                table: "Comments",
                column: "VotingBoxId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ForumSections_CreatedBy",
                table: "ForumSections",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ForumSections_ModifiedBy",
                table: "ForumSections",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_CreatedBy",
                table: "Submissions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ForumSectionId",
                table: "Submissions",
                column: "ForumSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ModifiedBy",
                table: "Submissions",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserId",
                table: "Submissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_VotingBoxId",
                table: "Submissions",
                column: "VotingBoxId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Upvotes_UserId",
                table: "Upvotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlacklistedPasswords");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Upvotes");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "ForumSections");

            migrationBuilder.DropTable(
                name: "VotingBoxes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
