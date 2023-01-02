using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ynventory.Data.Migrations
{
    /// <inheritdoc />
    public partial class importtasksremovecollectionfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportTasks_Collections_CollectionId",
                table: "ImportTasks");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "ImportTasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportTasks_Collections_CollectionId",
                table: "ImportTasks",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportTasks_Collections_CollectionId",
                table: "ImportTasks");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionId",
                table: "ImportTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportTasks_Collections_CollectionId",
                table: "ImportTasks",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
