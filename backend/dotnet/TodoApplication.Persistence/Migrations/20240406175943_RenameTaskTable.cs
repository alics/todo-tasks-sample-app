using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApplication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_Tasks_TaskId",
                table: "TaskHistories");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.CreateTable(
                name: "TodoTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BigInt", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DeadlineDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Status = table.Column<byte>(type: "TinyInt", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTasks", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_TodoTasks_TaskId",
                table: "TaskHistories",
                column: "TaskId",
                principalTable: "TodoTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistories_TodoTasks_TaskId",
                table: "TaskHistories");

            migrationBuilder.DropTable(
                name: "TodoTasks");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BigInt", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DeadlineDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Status = table.Column<byte>(type: "TinyInt", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistories_Tasks_TaskId",
                table: "TaskHistories",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
