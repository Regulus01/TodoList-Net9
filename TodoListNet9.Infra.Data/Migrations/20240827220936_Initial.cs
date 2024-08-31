using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ToDoList");

            migrationBuilder.CreateTable(
                name: "TaskLists",
                schema: "ToDoList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tal_Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskItem",
                schema: "ToDoList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tai_Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Tai_Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Tai_DueDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Tai_IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Tai_TaskListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItem_TaskLists_Tai_TaskListId",
                        column: x => x.Tai_TaskListId,
                        principalSchema: "ToDoList",
                        principalTable: "TaskLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_Tai_TaskListId",
                schema: "ToDoList",
                table: "TaskItem",
                column: "Tai_TaskListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskItem",
                schema: "ToDoList");

            migrationBuilder.DropTable(
                name: "TaskLists",
                schema: "ToDoList");
        }
    }
}
