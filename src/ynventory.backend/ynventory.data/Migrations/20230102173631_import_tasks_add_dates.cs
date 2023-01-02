using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ynventory.Data.Migrations
{
    /// <inheritdoc />
    public partial class importtasksadddates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "ImportTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "finishedAt",
                table: "ImportTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "ImportTasks");

            migrationBuilder.DropColumn(
                name: "finishedAt",
                table: "ImportTasks");
        }
    }
}
