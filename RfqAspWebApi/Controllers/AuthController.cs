// Controllers/AuthController.cs (ABSOLUTE MINIMUM EXAMPLE - For Learning Purposes)
// *** DO NOT USE IN PRODUCTION ***
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Needed for DbContext interaction
using System.Security.Cryptography; // Needed for password hashing
using System.Text; // Needed for Encoding
using System; // Needed for Convert, Exception
// We still need to reference the User entity and UserRole enum
// using RfqAspWebApi.Data; // Need AppDbContext
// using RfqAspWebApi.Models.Entities; // Need User entity
// using RfqAspWebApi.Models.Enums; // Need UserRole enum

[ApiController]
[Route("api/[controller]")] // Base route is /api/auth
public class AuthController : ControllerBase
{
    // Inject AppDbContext directly into the controller
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    // Simple synchronous password hashing (Utility logic directly in the controller)
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    // POST /api/auth/register
    // This endpoint accepts parameters directly from the request (e.g., query string, form data, or auto-mapped from JSON properties)
    // *** NO DTO IS USED FOR INPUT BINDING HERE ***
    [HttpPost("register")]
    public async Task<IActionResult> Register(string username, string password, UserRole role)
    {
        // --- Manual Basic Input Validation (Since no DTO with attributes) ---
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest(new { Message = "Username is required." });
        }
        if (string.IsNullOrEmpty(password))
        {
            // In a real scenario, you'd have password complexity rules here too
            return BadRequest(new { Message = "Password is required." });
        }
        // We rely on MVC to bind the 'role' parameter from the request (string or int) to the UserRole enum.
        // --- End Manual Validation ---


        // --- All Business Logic, Data Access, and Utility Calls are within the Controller Action ---

        try
        {
            // Check if username already exists (Business Logic / Data Access)
            if (await _context.Users.AnyAsync(u => u.Username == username))
            {
                return Conflict(new { Message = "Username already exists." }); // Use 409 Conflict for resource conflict
            }

            // Hash the password (Utility Logic Call)
            var passwordHash = HashPassword(password);

            // Create a new User entity *from* the directly provided input parameters
            var newUser = new User
            {
                Username = username,
                PasswordHash = passwordHash, // Store the HASHED password
                Role = role // Assign the bound enum value
            };

            // Add the new user entity to the database context (Data Access)
            _context.Users.Add(newUser);

            // Save changes to the database (Data Access)
            var result = await _context.SaveChangesAsync();

            // Determine HTTP response based on the outcome
            if (result > 0)
            {
                // Return 201 Created status on successful creation
                // In a real scenario, you might return a limited user object (without password hash)
                return StatusCode(201, new { Message = "User registered successfully." });
            }
            else
            {
                 // This case is less common if no exception occurs during SaveChangesAsync
                return StatusCode(500, new { Message = "An unexpected database error occurred during registration." });
            }
        }
        catch (Exception ex)
        {
            // Basic error handling - log the exception in a real app
            Console.Error.WriteLine($"An error occurred during registration: {ex.Message}");
            return StatusCode(500, new { Message = "An internal server error occurred." });
        }
        // --- End of Controller-Only Logic ---
    }

    // The Login action (POST /api/auth/login) would also reside here, accepting username/password directly,
    // verifying the password using the stored hash, and generating the JWT token, all within this controller action.
}