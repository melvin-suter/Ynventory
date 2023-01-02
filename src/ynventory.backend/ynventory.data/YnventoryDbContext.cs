using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Ynventory.Data.Models;

namespace Ynventory.Data
{
    public class YnventoryDbContext : DbContext
    {
        public YnventoryDbContext(DbContextOptions<YnventoryDbContext> options) : base(options) { }

        public DbSet<Collection> Collections { get; set; } = null!;
        public DbSet<CollectionItem> CollectionItems { get; set; } = null!;
        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<Deck> Decks { get; set; } = null!;
        public DbSet<DeckCard> DeckCards { get; set; } = null!;
        public DbSet<CardMetadata> CardMetadata { get; set; }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ImportError> ImportErrors { get; set; } = null!;
        public DbSet<ImportTask> ImportTasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseIdentityColumns();

            modelBuilder.Entity<Collection>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasMany(x => x.Items)
                       .WithOne(x => x.Collection)
                       .HasForeignKey(x => x.CollectionId);

                builder.ToTable("Collections");
            });

            modelBuilder.Entity<CollectionItem>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasMany(x => x.Cards)
                       .WithOne(x => x.ParentItem)
                       .HasForeignKey(x => x.ParentItemId);

                builder.HasOne(x => x.Collection)
                       .WithMany(x => x.Items)
                       .HasForeignKey(x => x.CollectionId);

                builder.ToTable("CollectionItems");
            });

            modelBuilder.Entity<Card>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasOne(x => x.Metadata)
                       .WithMany()
                       .HasForeignKey(x => x.MetadataId);

                builder.ToTable("Cards");
            });

            modelBuilder.Entity<Deck>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasMany(x => x.Cards)
                       .WithMany(x => x.Decks)
                       .UsingEntity<DeckCard>();

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

                builder.HasMany(x => x.Legalities)
                       .WithOne(x => x.Metadata)
                       .HasForeignKey(x => x.CardMetadataId);

                builder.HasMany(x => x.Decks)
                       .WithMany(x => x.Cards)
                       .UsingEntity<DeckCard>();

                builder.ToTable("CardMetadata");
            });

            modelBuilder.Entity<DeckCard>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasOne(x => x.Deck)
                       .WithMany()
                       .HasForeignKey(x => x.DeckId);

                builder.HasOne(x => x.Metadata)
                       .WithMany()
                       .HasForeignKey(x => x.MetadataId);

                builder.ToTable("DeckCards");
            });

            modelBuilder.Entity<CardColor>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasOne(x => x.Metadata)
                        .WithMany(x => x.Colors)
                        .HasForeignKey(x => x.CardMetadataId);

                builder.ToTable("CardColor");

            });

            modelBuilder.Entity<CardColorIdentity>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasOne(x => x.Metadata)
                       .WithMany(x => x.ColorIdentity)
                       .HasForeignKey(x => x.CardMetadataId);

                builder.ToTable("CardColorIdentity");
            });

            modelBuilder.Entity<CardKeyword>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasOne(x => x.Metadata)
                       .WithMany(x => x.Keywords)
                       .HasForeignKey(x => x.CardMetadataId);

                builder.ToTable("CardKeyword");
            });

            modelBuilder.Entity<CardLegality>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasOne(x => x.Metadata)
                       .WithMany(x => x.Legalities)
                       .HasForeignKey(x => x.CardMetadataId);

                builder.ToTable("CardLegality");
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.ToTable("Users");
            });


            modelBuilder.Entity<ImportTask>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();

                builder.HasMany(x => x.ImportErrors)
                       .WithOne(x => x.ImportTask)
                       .HasForeignKey(x => x.ImportTaskId);

                builder.ToTable("ImportTasks");
            });

            modelBuilder.Entity<ImportError>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityAlwaysColumn();


                builder.HasOne(x => x.ImportTask)
                       .WithMany(x => x.ImportErrors)
                       .HasForeignKey(x => x.ImportTaskId);

                builder.ToTable("ImportErrors");
            });
        }
    }
}
