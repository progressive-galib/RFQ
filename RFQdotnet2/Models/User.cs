using System.ComponentModel.DataAnnotations;

namespace YourProjectName.Models.Entities
{
    public class User
    {
        [Key] // Designates Id as the primary key
        public int Id { get; set; }

        [Required] // Makes the Username field NOT NULL
        [MaxLength(256)] // Optional: Set a max length
        public string Username { get; set; } = string.Empty; // Initialize with empty string

        [Required] // Makes the PasswordHash field NOT NULL
        public string PasswordHash { get; set; } = string.Empty; // Store hashed passwords

        [Required] // Makes the Role field NOT NULL
        [MaxLength(50)] // Optional: Set a max length
        // Consider using an enum or a separate Role entity for better type safety and data integrity
        // For this spec, we'll use string as per the table definition.
        public string Role { get; set; } = string.Empty;

        // Navigation properties (optional but helpful for EF Core)
        // A User can create multiple RFQs
        public ICollection<Rfq> CreatedRfqs { get; set; } = new List<Rfq>();
        // A User (as a Bidder) can submit multiple Quotations
        public ICollection<Quotation> SubmittedQuotations { get; set; } = new List<Quotation>();
    }
}
