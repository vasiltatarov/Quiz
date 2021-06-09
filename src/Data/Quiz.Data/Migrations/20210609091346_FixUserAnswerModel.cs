namespace Quiz.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixUserAnswerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Tests_TestId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_TestId",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "UserAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "UserAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_TestId",
                table: "UserAnswers",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Tests_TestId",
                table: "UserAnswers",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
