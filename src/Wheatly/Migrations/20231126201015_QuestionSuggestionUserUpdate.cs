using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wheatly.Migrations
{
    /// <inheritdoc />
    public partial class QuestionSuggestionUserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Suggestions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Suggestions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Questions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Questions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Questions");
        }
    }
}
