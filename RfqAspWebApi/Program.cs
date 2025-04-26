using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Needed for Configuration

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages(); 

// --- Add AppDbContext and configure SQLite ---
// Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the DbContext with DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));
// --- End DbContext Configuration ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- Add Authentication and Authorization Middleware (will be needed later for protected routes) ---
// app.UseAuthentication(); // Add after UseRouting, before UseAuthorization
// app.UseAuthorization();  // Add after UseAuthentication
// --- End Auth Middleware ---


app.MapControllers();

app.Run();