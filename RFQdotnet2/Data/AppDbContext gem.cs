using Microsoft.EntityFrameworkCore;
using YourProjectName.Models.Entities; // Import your entity classes

namespace YourProjectName.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions
        // This is how the database connection string and provider are passed in Program.cs
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet properties for each entity that maps to a database table
        public DbSet<User> Users { get; set; }
        public DbSet<Rfq> RFQs { get; set; } // Name matches the table name in your spec
        public DbSet<Quotation> Quotations { get; set; }

        // Optional: Configure relationships or constraints using the Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example of configuring the User-Rfq relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedRfqs) // A User has many CreatedRfqs
                .WithOne(r => r.Creator)     // An Rfq is created by one User
                .HasForeignKey(r => r.CreatedById) // The foreign key is CreatedById in Rfq
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a User if they have created RFQs

            // Example of configuring the User-Quotation relationship (Bidder)
             modelBuilder.Entity<User>()
                .HasMany(u => u.SubmittedQuotations) // A User has many SubmittedQuotations
                .WithOne(q => q.Bidder)          // A Quotation is submitted by one User
                .HasForeignKey(q => q.BidderId)  // The foreign key is BidderId in Quotation
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a User if they have submitted Quotations

            // Example of configuring the Rfq-Quotation relationship
             modelBuilder.Entity<Rfq>()
                .HasMany(r => r.Quotations)      // An Rfq has many Quotations
                .WithOne(q => q.Rfq)             // A Quotation is for one Rfq
                .HasForeignKey(q => q.RfqId)     // The foreign key is RfqId in Quotation
                .OnDelete(DeleteBehavior.Cascade); // If an Rfq is deleted, delete its Quotations

            // Configure the CHECK constraint for the Role column in the User table
            // Note: SQLite check constraints are not strictly enforced by default,
            // but defining it here is good practice and can be used by some tools.
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>(); // Ensure EF Core uses string for the enum/string property

            modelBuilder.Entity<User>()
                 .HasCheckConstraint("CK_User_Role", "[Role] IN ('employee', 'bidder')");

            // Ensure unique constraint on Username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
