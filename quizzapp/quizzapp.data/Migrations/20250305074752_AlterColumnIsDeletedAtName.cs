using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quizzapp.data.Migrations
{
    /// <inheritdoc />
    public partial class AlterColumnIsDeletedAtName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeletedAt",
                table: "Quizes",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeletedAt",
                table: "Questions",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeletedAt",
                table: "Answers",
                newName: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Quizes",
                newName: "IsDeletedAt");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Questions",
                newName: "IsDeletedAt");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Answers",
                newName: "IsDeletedAt");
        }
    }
}
