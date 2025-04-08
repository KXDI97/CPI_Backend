using Microsoft.EntityFrameworkCore;
using APIUsuarios.Models; // Reemplaza por el namespace correcto

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
}
