using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ynventory.Data.Models;

namespace Ynventory.Data
{
    public class YnventoryDbContext : DbContext
    {
        public YnventoryDbContext(DbContextOptions<YnventoryDbContext> options) : base(options) { }

        public DbSet<Collection> Collections { get; set; } = null!;
        public DbSet<Folder> Folders { get; set; } = null!;
        public DbSet<FolderCard> FolderCards { get; set; } = null!;
        public DbSet<Deck> Decks { get; set; } = null!;
        public DbSet<CardMetadata> CardMetadata { get; set; }
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Collection>(builder =>
            {
                builder.HasKey(x => x.Id)
                       .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                builder.HasMany(x => x.Folders)
                       .WithOne(x => x.Collection)
                       .HasForeignKey(x => x.CollectionId);

                builder.ToTable("Collections");
            });

            modelBuilder.Entity<Folder>(builder =>
            {
                builder.HasKey(x => x.Id)
                       .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                builder.HasMany(x => x.Cards)
                       .WithOne(x => x.Folder)
                       .HasForeignKey(x => x.FolderId);

                builder.HasOne(x => x.Collection)
                       .WithMany(x => x.Folders)
                       .HasForeignKey(x => x.CollectionId);

                builder.ToTable("Folders");
            });

            modelBuilder.Entity<FolderCard>(builder =>
            {
                builder.HasKey(x => x.Id)
                       .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                builder.HasOne(x => x.Metadata)
                       .WithMany()
                       .HasForeignKey(x => x.CardMetadataId);

                builder.HasMany(x => x.Decks)
                       .WithMany(x => x.Cards);

                builder.ToTable("FolderCards");
            });

            modelBuilder.Entity<Deck>(builder =>
            {
                builder.HasKey(x => x.Id)
                       .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                builder.HasMany(x => x.Cards)
                       .WithMany(x => x.Decks);

                builder.ToTable("Decks");
            });

            modelBuilder.Entity<CardMetadata>(builder =>
            {
                builder.HasKey(x => x.Id);

                builder.HasMany(x => x.Colors)
                       .WithOne(x => x.Metadata)
                       .HasForeignKey(x => x.CardMetadataId);

                builder.HasMany(x => x.ColorIdentity)
                       .WithOne(x => x.Metadata)
                       .HasForeignKey(x => x.CardMetadataId);

                builder.HasMany(x => x.Keywords)
                       .WithOne(x => x.Metadata)
                       .HasForeignKey(x => x.CardMetadataId);

                builder.ToTable("CardMetadata");
            });

            modelBuilder.Entity<CardColor>(builder =>
            {
                builder.HasKey(x => x.Id)
                       .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                builder.HasOne(x => x.Metadata)
                        .WithMany(x => x.Colors)
                        .HasForeignKey(x => x.CardMetadataId);

                builder.ToTable("CardColor");

            });

            modelBuilder.Entity<CardColorIdentity>(builder =>
            {
                builder.HasKey(x => x.Id)
                       .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                builder.HasOne(x => x.Metadata)
                       .WithMany(x => x.ColorIdentity)
                       .HasForeignKey(x => x.CardMetadataId);

                builder.ToTable("CardColorIdentity");
            });

            modelBuilder.Entity<CardKeyword>(builder =>
            {
                builder.HasKey(x => x.Id)
                       .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                builder.HasOne(x => x.Metadata)
                       .WithMany(x => x.Keywords)
                       .HasForeignKey(x => x.CardMetadataId);

                builder.ToTable("CardKeyword");
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(x => x.Id)
                       .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                builder.ToTable("Users");
            });
        }
    }
}
