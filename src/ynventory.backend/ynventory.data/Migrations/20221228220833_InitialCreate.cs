using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ynventory.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardMetadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Lang = table.Column<string>(type: "text", nullable: true),
                    Layout = table.Column<string>(type: "text", nullable: true),
                    ImageUrlSmall = table.Column<string>(type: "text", nullable: false),
                    ImageUrlLarge = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    ManaCost = table.Column<string>(type: "text", nullable: true),
                    OracleText = table.Column<string>(type: "text", nullable: true),
                    Power = table.Column<int>(type: "integer", nullable: true),
                    Toughness = table.Column<int>(type: "integer", nullable: true),
                    ManaCostTotal = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardMetadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardColor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Color = table.Column<string>(type: "text", nullable: false),
                    CardMetadataId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardColor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardColor_CardMetadata_CardMetadataId",
                        column: x => x.CardMetadataId,
                        principalTable: "CardMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardColorIdentity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ColorIdentity = table.Column<string>(type: "text", nullable: false),
                    CardMetadataId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardColorIdentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardColorIdentity_CardMetadata_CardMetadataId",
                        column: x => x.CardMetadataId,
                        principalTable: "CardMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardKeyword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Keyword = table.Column<string>(type: "text", nullable: false),
                    CardMetadataId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardKeyword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardKeyword_CardMetadata_CardMetadataId",
                        column: x => x.CardMetadataId,
                        principalTable: "CardMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CollectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folders_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FolderCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    CardMetadataId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    FolderId = table.Column<int>(type: "integer", nullable: false),
                    Finish = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolderCards_CardMetadata_CardMetadataId",
                        column: x => x.CardMetadataId,
                        principalTable: "CardMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderCards_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeckFolderCard",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "integer", nullable: false),
                    DecksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckFolderCard", x => new { x.CardsId, x.DecksId });
                    table.ForeignKey(
                        name: "FK_DeckFolderCard_Decks_DecksId",
                        column: x => x.DecksId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckFolderCard_FolderCards_CardsId",
                        column: x => x.CardsId,
                        principalTable: "FolderCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardColor_CardMetadataId",
                table: "CardColor",
                column: "CardMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_CardColorIdentity_CardMetadataId",
                table: "CardColorIdentity",
                column: "CardMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_CardKeyword_CardMetadataId",
                table: "CardKeyword",
                column: "CardMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckFolderCard_DecksId",
                table: "DeckFolderCard",
                column: "DecksId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderCards_CardMetadataId",
                table: "FolderCards",
                column: "CardMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderCards_FolderId",
                table: "FolderCards",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_CollectionId",
                table: "Folders",
                column: "CollectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardColor");

            migrationBuilder.DropTable(
                name: "CardColorIdentity");

            migrationBuilder.DropTable(
                name: "CardKeyword");

            migrationBuilder.DropTable(
                name: "DeckFolderCard");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "FolderCards");

            migrationBuilder.DropTable(
                name: "CardMetadata");

            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DropTable(
                name: "Collections");
        }
    }
}
