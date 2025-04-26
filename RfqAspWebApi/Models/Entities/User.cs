// User.cs
using System.Collections.Generic; // Needed for ICollection

public enum UserRole
{
    Employee, // By default, this is assigned the integer value 0
    Bidder    // By default, this is assigned the integer value 1
}

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; } // TEXT, UNIQUE, NOT NULL
    public required string PasswordHash { get; set; } // TEXT, NOT NULL
    public UserRole Role { get; set; } // TEXT, NOT NULL (e.g., 'employee', 'bidder')

    // Navigation property for RFQs created by this user
    public ICollection<Rfq> CreatedRfqs { get; set; } = new List<Rfq>();

    // Navigation property for Quotations submitted by this user
    public ICollection<Quotation> SubmittedQuotations { get; set; } = new List<Quotation>();
}