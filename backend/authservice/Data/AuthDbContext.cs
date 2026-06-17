using authservice.Models;
using Microsoft.EntityFrameworkCore;
namespace authservice.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<Usuario> Usuarios {get; set;}
}