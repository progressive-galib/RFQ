// Rfq.cs
using System; // Needed for DateTime
using System.Collections.Generic; // Needed for ICollection

public class Rfq
{
    public int Id { get; set; }
    public string Title { get; set; } // TEXT, NOT NULL
    public string Description { get; set; } // TEXT, NOT NULL
    public int CreatedById { get; set; } // INTEGER, FOREIGN KEY
    public DateTime CreatedAt { get; set; } // DATETIME, NOT NULL

    // Navigation property to the User who created this RFQ
    public User CreatedBy { get; set; }

    // Navigation property for Quotations submitted for this RFQ
    public ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();
}