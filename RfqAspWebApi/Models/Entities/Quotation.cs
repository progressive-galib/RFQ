// Quotation.cs
using System; // Needed for DateTime

public class Quotation
{
    public int Id { get; set; }
    public int RfqId { get; set; } // INTEGER, FOREIGN KEY
    public int BidderId { get; set; } // INTEGER, FOREIGN KEY
    public decimal Price { get; set; } // REAL in SQLite, using decimal in C# for currency
    public string Details { get; set; } // TEXT
    public DateTime SubmittedAt { get; set; } // DATETIME, NOT NULL

    // Navigation property to the RFQ this quotation is for
    public Rfq Rfq { get; set; }

    // Navigation property to the User (Bidder) who submitted this quotation
    public User Bidder { get; set; }
}