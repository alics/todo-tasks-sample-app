using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApplication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTaskTitleFieldSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeadlineDate",
                table: "TodoTasks",
                newName: "Deadline");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TodoTasks",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "TodoTasks",
                newName: "DeadlineDate");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TodoTasks",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");
        }
    }
}
