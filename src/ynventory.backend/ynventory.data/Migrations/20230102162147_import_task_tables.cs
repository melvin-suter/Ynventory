using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ynventory.Data.Migrations
{
    /// <inheritdoc />
    public partial class importtasktables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    TaskState = table.Column<int>(type: "integer", nullable: false),
                    TaskType = table.Column<int>(type: "integer", nullable: false),
                    FileData = table.Column<byte[]>(type: "bytea", nullable: false),
                    CollectionId = table.Column<int>(type: "integer", nullable: false),
                    CollectionItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportTasks_CollectionItems_CollectionItemId",
                        column: x => x.CollectionItemId,
                        principalTable: "CollectionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportTasks_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportErrors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ErrorData = table.Column<string>(type: "text", nullable: false),
                    Error = table.Column<string>(type: "text", nullable: false),
                    ImportTaskId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportErrors_ImportTasks_ImportTaskId",
                        column: x => x.ImportTaskId,
                        principalTable: "ImportTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImportErrors_ImportTaskId",
                table: "ImportErrors",
                column: "ImportTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportTasks_CollectionId",
                table: "ImportTasks",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportTasks_CollectionItemId",
                table: "ImportTasks",
                column: "CollectionItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportErrors");

            migrationBuilder.DropTable(
                name: "ImportTasks");
        }
    }
}
