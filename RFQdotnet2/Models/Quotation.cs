using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // For ForeignKey

namespace YourProjectName.Models.Entities
{
    public class Quotation
    {
        [Key] // Designates Id as the primary key
        public int Id { get; set; }

        [Required] // Makes the RfqId field NOT NULL
        [ForeignKey("Rfq")] // Designates RfqId as a foreign key referencing the Rfq entity
        public int RfqId { get; set; }

        [Required] // Makes the BidderId field NOT NULL
        [ForeignKey("Bidder")] // Designates BidderId as a foreign key referencing the User entity
        public int BidderId { get; set; }

        [Required] // Makes the Price field NOT NULL
        public double Price { get; set; } // Using double for REAL in SQLite

        // Details can be nullable as per schema (TEXT)
        public string? Details { get; set; } // Use string? for nullable string

        [Required] // Makes the SubmittedAt field NOT NULL
        public DateTime SubmittedAt { get; set; }

        // Navigation properties
        public required Rfq Rfq { get; set; } // The RFQ this quotation is for
        public required User  Bidder { get; set; } // The User who submitted this quotation
    }
}