﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ynventory.Data;

#nullable disable

namespace Ynventory.Data.Migrations
{
    [DbContext(typeof(YnventoryDbContext))]
    [Migration("20221223232838_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("DeckFolderCard", b =>
                {
                    b.Property<int>("CardsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DecksId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CardsId", "DecksId");

                    b.HasIndex("DecksId");

                    b.ToTable("DeckFolderCard");
                });

            modelBuilder.Entity("Ynventory.Data.Models.CardColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CardMetadataId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasIndex("CardMetadataId");

                    b.ToTable("CardColor", (string)null);
                });

            modelBuilder.Entity("Ynventory.Data.Models.CardColorIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CardMetadataId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ColorIdentity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasIndex("CardMetadataId");

                    b.ToTable("CardColorIdentity", (string)null);
                });

            modelBuilder.Entity("Ynventory.Data.Models.CardKeyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CardMetadataId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Keyword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasIndex("CardMetadataId");

                    b.ToTable("CardKeyword", (string)null);
                });

            modelBuilder.Entity("Ynventory.Data.Models.CardMetadata", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrlLarge")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrlSmall")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lang")
                        .HasColumnType("TEXT");

                    b.Property<string>("Layout")
                        .HasColumnType("TEXT");

                    b.Property<string>("ManaCost")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ManaCostTotal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OracleText")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Power")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Toughness")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CardMetadata", (string)null);
                });

            modelBuilder.Entity("Ynventory.Data.Models.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.ToTable("Collections", (string)null);
                });

            modelBuilder.Entity("Ynventory.Data.Models.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.ToTable("Decks", (string)null);
                });

            modelBuilder.Entity("Ynventory.Data.Models.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CollectionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasIndex("CollectionId");

                    b.ToTable("Folders", (string)null);
                });

            modelBuilder.Entity("Ynventory.Data.Models.FolderCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CardMetadataId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Finish")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FolderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasIndex("CardMetadataId");

                    b.HasIndex("FolderId");

                    b.ToTable("FolderCards", (string)null);
                });

            modelBuilder.Entity("Ynventory.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DeckFolderCard", b =>
                {
                    b.HasOne("Ynventory.Data.Models.FolderCard", null)
                        .WithMany()
                        .HasForeignKey("CardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ynventory.Data.Models.Deck", null)
                        .WithMany()
                        .HasForeignKey("DecksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ynventory.Data.Models.CardColor", b =>
                {
                    b.HasOne("Ynventory.Data.Models.CardMetadata", "Metadata")
                        .WithMany("Colors")
                        .HasForeignKey("CardMetadataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("Ynventory.Data.Models.CardColorIdentity", b =>
                {
                    b.HasOne("Ynventory.Data.Models.CardMetadata", "Metadata")
                        .WithMany("ColorIdentity")
                        .HasForeignKey("CardMetadataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("Ynventory.Data.Models.CardKeyword", b =>
                {
                    b.HasOne("Ynventory.Data.Models.CardMetadata", "Metadata")
                        .WithMany("Keywords")
                        .HasForeignKey("CardMetadataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("Ynventory.Data.Models.Folder", b =>
                {
                    b.HasOne("Ynventory.Data.Models.Collection", "Collection")
                        .WithMany("Folders")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("Ynventory.Data.Models.FolderCard", b =>
                {
                    b.HasOne("Ynventory.Data.Models.CardMetadata", "Metadata")
                        .WithMany()
                        .HasForeignKey("CardMetadataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ynventory.Data.Models.Folder", "Folder")
                        .WithMany("Cards")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("Ynventory.Data.Models.CardMetadata", b =>
                {
                    b.Navigation("ColorIdentity");

                    b.Navigation("Colors");

                    b.Navigation("Keywords");
                });

            modelBuilder.Entity("Ynventory.Data.Models.Collection", b =>
                {
                    b.Navigation("Folders");
                });

            modelBuilder.Entity("Ynventory.Data.Models.Folder", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
