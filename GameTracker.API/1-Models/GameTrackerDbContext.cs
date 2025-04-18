using Microsoft.EntityFrameworkCore;

namespace GameTracker.Models;
public class GameTrackerDbContext : DbContext
{
    public DbSet<Store> Stores { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GameConsole> GameConsoles { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<GameTag> GameTags { get; set; }

    public GameTrackerDbContext(DbContextOptions<GameTrackerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Store
        modelBuilder.Entity<Store>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Store>()
            .HasMany(s => s.Games)
            .WithOne(g => g.Store)
            .HasForeignKey(g => g.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Store>()
            .HasMany(s => s.GameConsoles)
            .WithOne(gc => gc.Store)
            .HasForeignKey(gc => gc.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        // Game
        modelBuilder.Entity<Game>()
            .Property(g => g.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Game>()
            .HasMany(g => g.GameTags)
            .WithOne(gt => gt.Game)
            .HasForeignKey(gt => gt.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        // GameConsole
        modelBuilder.Entity<GameConsole>()
            .Property(gc => gc.Id)
            .ValueGeneratedOnAdd();

        // Worker
        modelBuilder.Entity<Worker>()
            .Property(w => w.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Worker>()
            .HasMany(w => w.Checkouts)
            .WithOne(c => c.Worker)
            .HasForeignKey(c => c.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Checkout
        modelBuilder.Entity<Checkout>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Checkout>()
            .HasOne(c => c.Game)
            .WithMany()
            .HasForeignKey(c => c.GameId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Checkout>()
            .HasOne(c => c.GameConsole)
            .WithMany()
            .HasForeignKey(c => c.GameConsoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Tag
        modelBuilder.Entity<Tag>()
            .HasMany(t => t.GameTags)
            .WithOne(gt => gt.Tag)
            .HasForeignKey(gt => gt.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        // GameTag (many-to-many)
        modelBuilder.Entity<GameTag>()
            .HasKey(gt => new { gt.GameId, gt.TagId });

        modelBuilder.Entity<GameTag>()
            .HasOne(gt => gt.Game)
            .WithMany(g => g.GameTags)
            .HasForeignKey(gt => gt.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GameTag>()
            .HasOne(gt => gt.Tag)
            .WithMany(t => t.GameTags)
            .HasForeignKey(gt => gt.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}