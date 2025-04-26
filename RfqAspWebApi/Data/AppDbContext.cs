// Data/AppDbContext.cs (Updated)
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion; // Needed for ValueConverter

// Assuming Entity classes and UserRole enum are accessible
// using RfqAspWebApi.Models.Entities;
// using RfqAspWebApi.Models.Enums; // Example namespace for UserRole

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Rfq> Rfqs { get; set; }
    public DbSet<Quotation> Quotations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User.Username for uniqueness
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        // Configure EF Core to store the UserRole enum as a string
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>(); // Use HasConversion<string>()

        // Configure relationships... (Existing code for relationships goes here)
        modelBuilder.Entity<Rfq>()
            .HasOne(r => r.CreatedBy)
            .WithMany(u => u.CreatedRfqs)
            .HasForeignKey(r => r.CreatedById);

        modelBuilder.Entity<Quotation>()
            .HasOne(q => q.Rfq)
            .WithMany(r => r.Quotations)
            .HasForeignKey(q => q.RfqId);

        modelBuilder.Entity<Quotation>()
            .HasOne(q => q.Bidder)
            .WithMany(u => u.SubmittedQuotations)
            .HasForeignKey(q => q.BidderId);

        // Note: The database-level CHECK constraint on Role is separate from this EF Core mapping.
        // The EF Core mapping ensures the C# enum correctly reads/writes string values.
        // The CHECK constraint would ideally be added as raw SQL in a migration to
        // ensure data integrity even outside the application's EF Core operations.
    }
}