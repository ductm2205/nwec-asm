using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserQuizTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_UserQuizzes_UserQuizId",
                schema: "common",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuizzes",
                schema: "common",
                table: "UserQuizzes");

            migrationBuilder.AlterColumn<string>(
                name: "QuizCode",
                schema: "common",
                table: "UserQuizzes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "common",
                table: "UserQuizzes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuizzes",
                schema: "common",
                table: "UserQuizzes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_UserQuizzes_UserQuizId",
                schema: "common",
                table: "UserAnswers",
                column: "UserQuizId",
                principalSchema: "common",
                principalTable: "UserQuizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_UserQuizzes_UserQuizId",
                schema: "common",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuizzes",
                schema: "common",
                table: "UserQuizzes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "common",
                table: "UserQuizzes");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuizCode",
                schema: "common",
                table: "UserQuizzes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuizzes",
                schema: "common",
                table: "UserQuizzes",
                column: "QuizCode");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_UserQuizzes_UserQuizId",
                schema: "common",
                table: "UserAnswers",
                column: "UserQuizId",
                principalSchema: "common",
                principalTable: "UserQuizzes",
                principalColumn: "QuizCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
