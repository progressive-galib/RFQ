using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // For ForeignKey

namespace YourProjectName.Models.Entities
{
    public class Rfq
    {
        [Key] // Designates Id as the primary key
        public int Id { get; set; }

        [Required] // Makes the Title field NOT NULL
        [MaxLength(500)] // Optional: Set a max length
        public string Title { get; set; } = string.Empty;

        [Required] // Makes the Description field NOT NULL
        public string Description { get; set; } = string.Empty;

        [Required] // Makes the CreatedById field NOT NULL
        [ForeignKey("Creator")] // Designates CreatedById as a foreign key referencing the User entity
        public int CreatedById { get; set; }

        [Required] // Makes the CreatedAt field NOT NULL
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public required User Creator { get; set; } // The User who created this RFQ
        public ICollection<Quotation> Quotations { get; set; } = new List<Quotation>(); // Quotations submitted for this RFQ
    }
}