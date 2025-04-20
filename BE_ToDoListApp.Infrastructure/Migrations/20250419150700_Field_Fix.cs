using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_ToDoListApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Field_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descrption",
                table: "ToDoTask",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ToDoTask",
                newName: "Descrption");
        }
    }
}
