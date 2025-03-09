using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quizzapp.data.Migrations
{
    /// <inheritdoc />
    public partial class AlterQuestionsQuizzIdToQuizId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_QuizzId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuizzId",
                table: "Questions",
                newName: "QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuizzId",
                table: "Questions",
                newName: "IX_Questions_QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "Questions",
                newName: "QuizzId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                newName: "IX_Questions_QuizzId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_QuizzId",
                table: "Questions",
                column: "QuizzId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
