using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SifflForums.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixStringForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Submissions_SubmissionId1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_ForumSections_ForumSectionId1",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_ForumSectionId1",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SubmissionId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ForumSectionId1",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "SubmissionId1",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "ForumSectionId",
                table: "Submissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SubmissionId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ForumSectionId",
                table: "Submissions",
                column: "ForumSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SubmissionId",
                table: "Comments",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Submissions_SubmissionId",
                table: "Comments",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_ForumSections_ForumSectionId",
                table: "Submissions",
                column: "ForumSectionId",
                principalTable: "ForumSections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Submissions_SubmissionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_ForumSections_ForumSectionId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_ForumSectionId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SubmissionId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ForumSectionId",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForumSectionId1",
                table: "Submissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubmissionId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmissionId1",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ForumSectionId1",
                table: "Submissions",
                column: "ForumSectionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SubmissionId1",
                table: "Comments",
                column: "SubmissionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Submissions_SubmissionId1",
                table: "Comments",
                column: "SubmissionId1",
                principalTable: "Submissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_ForumSections_ForumSectionId1",
                table: "Submissions",
                column: "ForumSectionId1",
                principalTable: "ForumSections",
                principalColumn: "Id");
        }
    }
}
