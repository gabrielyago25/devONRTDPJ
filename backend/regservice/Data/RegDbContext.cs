using Microsoft.EntityFrameworkCore;
using regservice.Models;

namespace regservice.Data;

public class RegDbContext : DbContext
{
    public RegDbContext(DbContextOptions<RegDbContext> options)
    : base(options)
    {
        
    }
    public DbSet<Registro> Registros {get; set;}
    
}